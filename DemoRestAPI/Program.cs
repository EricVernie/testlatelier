using Microsoft.AspNetCore.Http.HttpResults;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Register HttpClient
builder.Services.AddHttpClient();
builder.Services.AddTransient<ServicePlayers>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Crée un endpoint qui retourne les joueurs. La liste doit être triée du meilleur au moins bon.

//TODO : AJouter attribut authorisation
app.MapGet("/Players", async (ServicePlayers servicePlayers ) =>
{
    var RootObject = await servicePlayers.GetPlayersAsync();
    return Results.Ok(RootObject.players.OrderBy (p => p.data.rank));
    
})
.WithName("GetJoueurs")
.WithOpenApi();

app.MapGet("/Players/{id}", async (string id, ServicePlayers servicePlayers) =>
{
    var player = await servicePlayers.GetPlayerByIdAsync(id);
    if (player == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(player);
})
.WithName("GetPlayerById")
.WithOpenApi();

// Crée un endpoint qui retourne les statistiques des joueurs.
app.MapGet("/Players/Statistics", async (ServicePlayers servicePlayers) =>
{
    var RootObject = await servicePlayers.GetPlayersAsync();

    // Pays avec le plus grand ratio de parties gagnées
    var countryWithBestWinRatio = RootObject.players
        .GroupBy(p => p.country.code)
        .Select(g => new
        {
            Code=g.Select(p => p.country.code).First(),
            WinRatio = g.Sum(p => p.data.points) 
        })
        .OrderByDescending(g => g.WinRatio)
        .FirstOrDefault();

    // IMC moyen de tous les joueurs
    var averageBMI = RootObject.players
        .Select(p => p.data.weight / (p.data.height * p.data.height))
        .Average();

    // Médiane de la taille des joueurs
    var medianHeight = RootObject.players
        .Select(p => p.data.height)
        .OrderBy(h => h)
        .ToList();
    double median;
    int count = medianHeight.Count;
    if (count % 2 == 0)
    {
        median = (medianHeight[count / 2 - 1] + medianHeight[count / 2]) / 2;
    }
    else
    {
        median = medianHeight[count / 2];
    }

    var statistics = new
    {
        CountryWithBestWinRatio = countryWithBestWinRatio,
        AverageBMI = averageBMI,
        MedianHeight = median
    };

    return Results.Ok(statistics);
})
.WithName("GetPlayerStatistics")
.WithOpenApi();

//app.UseHttpsRedirection();



app.Run();



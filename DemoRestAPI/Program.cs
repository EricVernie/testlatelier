using Microsoft.AspNetCore.Http.HttpResults;

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

app.MapGet("/Players", async (ServicePlayers servicePlayers ) =>
{
    var Players = await servicePlayers.GetPlayerAsync();
    

    return Results.Ok(Players.players.OrderBy (p => p.data.rank));
    
})
.WithName("GetJoueurs")
.WithOpenApi();

//app.UseHttpsRedirection();



app.Run();



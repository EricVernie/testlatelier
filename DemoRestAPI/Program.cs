var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Crée un endpoint qui retourne les joueurs. La liste doit être triée du meilleur au moins bon.
var joueurs = new List<Joueur>
{
    new Joueur { Nom = "Joueur1", Score = 100 },
    new Joueur { Nom = "Joueur2", Score = 200 },
    new Joueur { Nom = "Joueur3", Score = 150 }
};

app.MapGet("/joueurs", () =>
{
    return joueurs.OrderByDescending(j => j.Score);
})
.WithName("GetJoueurs")
.WithOpenApi();

//app.UseHttpsRedirection();



app.Run();


internal record Joueur
{
    public string Nom { get; set; }
    public int Score { get; set; }
};
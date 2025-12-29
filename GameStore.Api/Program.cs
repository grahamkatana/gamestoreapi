using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<GameDto> games = [
    new (1, "The Legend of Zelda: Breath of the Wild", "Action-adventure", 59.99m, new DateOnly(2017, 3, 3)),
    new (2, "God of War", "Action-adventure", 49.99m, new DateOnly(2018, 4, 20)),
    new (3, "Red Dead Redemption 2", "Action-adventure", 69.99m, new DateOnly(2018, 10, 26)),
    new (4, "Minecraft", "Sandbox", 26.95m, new DateOnly(2011, 11, 18)),
    new (5, "The Witcher 3: Wild Hunt", "Action RPG", 39.99m, new DateOnly(2015, 5, 19))
];


// GET /games
app.MapGet("/games", () => games);

app.Run();

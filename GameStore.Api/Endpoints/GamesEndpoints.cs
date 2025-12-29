using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string EndpointName = "GetGameById";
    private static readonly List<GameDto> games = [
         new (1, "The Legend of Zelda: Breath of the Wild", "Action-adventure", 59.99m, new DateOnly(2017, 3, 3)),
    new (2, "God of War", "Action-adventure", 49.99m, new DateOnly(2018, 4, 20)),
    new (3, "Red Dead Redemption 2", "Action-adventure", 69.99m, new DateOnly(2018, 10, 26)),
    new (4, "Minecraft", "Sandbox", 26.95m, new DateOnly(2011, 11, 18)),
    new (5, "The Witcher 3: Wild Hunt", "Action RPG", 39.99m, new DateOnly(2015, 5, 19))
     ];

    public static void MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");
        // GET /games
        group.MapGet("/", () => games);

        // GET /games/{id}
        group.MapGet("/{id}", (int id) =>
        {
            var game = games.FirstOrDefault(g => g.Id == id);
            return game is not null ? Results.Ok(game) : Results.NotFound();
        }).WithName(EndpointName);

        // POST /games
        group.MapPost("/", (GameCreateDto newGame) =>
        {
            var nextId = games.Max(g => g.Id) + 1;
            var gameDto = new GameDto(
        nextId,
        newGame.Title,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );
            games.Add(gameDto);
            return Results.CreatedAtRoute(EndpointName, new { id = gameDto.Id }, gameDto);
        });

        // PATCH /games/{id}
        group.MapPatch("/{id}", (int id, GameUpdateDto updatedGame) =>
        {

            var index = games.FindIndex(g => g.Id == id);
            if (index == -1) return Results.NotFound();
            games[index] = new GameDto(
                id,
                updatedGame.Title,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );
            return Results.Ok();

        });

        // DELETE /games/{id}
        group.MapDelete("/{id}", (int id) =>
        {
            var index = games.FindIndex(g => g.Id == id);
            if (index == -1) return Results.NotFound();
            games.RemoveAt(index);
            return Results.NoContent();
        });

    }
}

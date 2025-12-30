using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

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
        group.MapGet("/", async (GameStoreContext dbContext) =>
        {
            var games = await dbContext.Games.Include(game => game.Genre)
                .Select(game => new GameDto(
                    game.Id,
                    game.Title,
                    game.Genre!.Name,
                    game.Price,
                    game.ReleaseDate
                ))
                .ToListAsync();
            return Results.Ok(games);
        });

        // GET /games/{id}
        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            var game = await dbContext.Games.FindAsync(id);

            return game is not null ? Results.Ok(
                new GameDetailsDto(
                    game.Id,
                    game.Title,
                    game.GenreId,
                    game.Price,
                    game.ReleaseDate
                )
            ) : Results.NotFound();
        }).WithName(EndpointName);

        // POST /games
        group.MapPost("/", async (GameCreateDto newGame, GameStoreContext dbContext) =>
        {
    
            Game game = new()
            {
                Title = newGame.Title,
                GenreId = newGame.GenreId,
                Price = newGame.Price,
                ReleaseDate = newGame.ReleaseDate
            };
            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();
            GameDetailsDto gameDto = new(
                game.Id,
                game.Title,
                game.GenreId,
                game.Price,
                game.ReleaseDate
            );
            return Results.CreatedAtRoute(EndpointName, new { id = gameDto.Id }, gameDto);

        });

        // PATCH /games/{id}
        group.MapPatch("/{id}", async (int id, GameUpdateDto updatedGame, GameStoreContext dbContext) =>
        {
            var game = await dbContext.Games.FindAsync(id);
            if (game is null) return Results.NotFound();
            game.Title =  updatedGame.Title;
            game.GenreId = updatedGame.GenreId;
            game.Price = updatedGame.Price;
            game.ReleaseDate = updatedGame.ReleaseDate;
            await dbContext.SaveChangesAsync();
            return Results.Ok();

        });

        // DELETE /games/{id}
        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            var game = await dbContext.Games.FindAsync(id);
            if (game is null) return Results.NotFound();
            dbContext.Games.Remove(game);
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        });

    }
}

using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string EndpointName = "GetGameById";

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
            await dbContext.Games.Where(g => g.Id == id).ExecuteDeleteAsync();
            return Results.NoContent();
        });

    }
}

using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;


namespace GameStore.Api.Endpoints;

public static class GenresEndpoints
{
    public static void MapGenresEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/genres");

        // GET /genres
        group.MapGet("/", async (GameStoreContext dbContext) =>
        {
            var genres = await dbContext.Genres
                .Select(genre => new GenreDto(
                    genre.Id,
                    genre.Name
                ))
                .ToListAsync();
            return Results.Ok(genres);
        });
        
    }

}

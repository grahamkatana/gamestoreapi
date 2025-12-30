

using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        db.Database.Migrate();

    }

    public static void AddGameStoreDb(this WebApplicationBuilder builder)
    {
        var connString = builder.Configuration.GetConnectionString("GameStoreDb")
            ?? "Data Source=gamestore.db";
        builder.Services.AddScoped<GameStoreContext>();
        builder.Services.AddSqlite<GameStoreContext>(
            connString,
            optionsAction: options => options.UseSeeding((context, _) =>
            {
                if (!context.Set<Genre>().Any())
                {
                    context.Set<Genre>().AddRange(
                        new Genre { Name = "Action" },
                        new Genre { Name = "Adventure" },
                        new Genre { Name = "RPG" },
                        new Genre { Name = "Strategy" },
                        new Genre { Name = "Simulation" },
                        new Genre { Name = "Sports" },
                        new Genre { Name = "Puzzle" },
                        new Genre { Name = "Horror" },
                        new Genre { Name = "Indie" }
                    );
                    context.SaveChanges();

                }
            })
        );

    }

}

using GamersWorld.Application.Common.Interfaces;
using GamersWorld.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GamersWorld.Data.Contexts;

public class GamersWorldDbContext(DbContextOptions<GamersWorldDbContext> dbContextOptions)
    : DbContext(dbContextOptions), IApplicationDbContext
{
    public DbSet<Game> Games { get; set; }
    IQueryable<Game> IApplicationDbContext.Games => Games;

    public async Task<Game> AddGameAsync(Game game)
    {
        _ = await Games.AddAsync(game);
        return game;
    }

    public async Task<Game> FindGameAsync(int id, CancellationToken cancellationToken)
    {
        return await Games.FindAsync([id], cancellationToken: cancellationToken);
    }

    public void RemoveGame(Game game)
    {
        Games.Remove(game);
    }
}
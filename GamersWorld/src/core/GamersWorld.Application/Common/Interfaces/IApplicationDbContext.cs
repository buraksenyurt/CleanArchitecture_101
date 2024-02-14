using GamersWorld.Domain.Entities;

namespace GamersWorld.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    IQueryable<Game> Games { get; }
    Task<Game> FindGameAsync(int id, CancellationToken cancellationToken);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    Task<Game> AddGameAsync(Game game);
    void RemoveGame(Game game);
}
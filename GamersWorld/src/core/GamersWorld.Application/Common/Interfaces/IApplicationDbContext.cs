using GamersWorld.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GamersWorld.Application.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<Game> Games { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
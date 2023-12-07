using GamersWorld.Application.Common.Interfaces;
using GamersWorld.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GamersWorld.Data.Contexts;

public class GamersWorldDbContext(DbContextOptions<GamersWorldDbContext> dbContextOptions)
    : DbContext(dbContextOptions), IApplicationDbContext
{
    public DbSet<Game> Games { get; set; }
}
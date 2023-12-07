using GamersWorld.Application.Common.Interfaces;
using GamersWorld.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GamersWorld.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<GamersWorldDbContext>(options => options.UseSqlite(configuration.GetConnectionString("DevConStr")));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<GamersWorldDbContext>());
        return services;
    }
}
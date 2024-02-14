using GamersWorld.Application.Common.Interfaces;
using GamersWorld.Domain.Settings;
using GamersWorld.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GamersWorld.Shared;

public static class DependencyInjection
{
    public static IServiceCollection AddShared(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IEmailService, EMailService>();
        services.AddTransient<IExportBuilder, CsvExportBuilder>();
        services.AddTransient<IImageHandler, ImageHandler>();
        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));

        return services;
    }
}
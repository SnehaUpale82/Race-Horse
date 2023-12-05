using System.Reflection;
using Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Application;

public static class DependancyInjection
{

    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddSingleton<IRacingService, RacingService>();
        services.AddSingleton<IRacingBackgroundService, RacingBackgroundService>();
        services.AddSingleton<IHostedService>(p => p.GetService<IRacingBackgroundService>());
        return services;
    }

}
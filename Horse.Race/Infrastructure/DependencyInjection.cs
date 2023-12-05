using Domain.Interface;
using Infrastructure.Configuration;
using Infrastructure.Dapper;
using Infrastructure.Dapper.Interface;
using Infrastructure.Http;
using Infrastructure.Http.Interface;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.Configure<DatabaseConfiguration>(configuration.GetSection(DatabaseConfiguration.ConfigurationName))
            .AddSingleton<IRacingDapperConnectionFactory, RacingDapperConnectionFactory>()
            .AddSingleton<IDapperWrapper, DapperWrapper>();

        services.AddHttpClient<IRaceApiClient, RaceApiClient>();
        services.Configure<RaceApiConfiguration>(
            configuration.GetSection(RaceApiConfiguration.ConfigurationName));
        services.AddScoped<IRaceRepository, RaceRepository>();
        services.AddScoped<IDbRaceRepository, DbRaceRepository>();


        return services;
    }
}
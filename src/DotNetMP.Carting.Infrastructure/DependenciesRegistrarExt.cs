using DotNetMP.Carting.Infrastructure.Data;
using DotNetMP.Carting.Infrastructure.Data.Options;
using DotNetMP.SharedKernel.Interfaces;
using LiteDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetMP.Carting.Infrastructure;

public static class DependenciesRegistrarExt
{
    public static IServiceCollection RegisterInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<LiteDbOptions>()
            .Bind(configuration.GetSection(LiteDbOptions.Path));

        services.AddScoped(typeof(IRepository<>), typeof(LiteDbRepository<>));
        services.AddSingleton<IClientFactory<ILiteDatabase>, LiteDatabaseFactory>();

        return services;
    }
}

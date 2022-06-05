using DotNetMP.Carting.Core;
using DotNetMP.Carting.Infrastructure;

namespace DotNetMP.Carting.WebApi;

public static class DependenciesRegistrarExt
{
    public static IServiceCollection RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterCoreDependencies();
        services.RegisterInfrastructureDependencies(configuration);
        return services;
    }
}

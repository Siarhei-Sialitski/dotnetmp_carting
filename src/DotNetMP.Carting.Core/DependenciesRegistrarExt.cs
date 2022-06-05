using DotNetMP.Carting.Core.Interfaces;
using DotNetMP.Carting.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetMP.Carting.Core;

public static class DependenciesRegistrarExt
{
    public static IServiceCollection RegisterCoreDependencies(this IServiceCollection services)
    {
        services.AddScoped<ICartCommandService, CartCommandService>();
        services.AddScoped<ICartQueryService, CartQueryService>();

        return services;
    }
}

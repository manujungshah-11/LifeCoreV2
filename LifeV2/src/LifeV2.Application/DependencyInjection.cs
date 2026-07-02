using LifeV2.Application.Interfaces;
using LifeV2.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LifeV2.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IPolicyService, PolicyService>();
        return services;
    }
}

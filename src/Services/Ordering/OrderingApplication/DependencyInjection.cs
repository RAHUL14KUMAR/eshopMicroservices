using Microsoft.Extensions.DependencyInjection;

namespace OrderingApplication;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        // Add application services here

        return services;
    }
}
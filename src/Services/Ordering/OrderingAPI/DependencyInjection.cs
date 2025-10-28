namespace OrderingAPI;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        // Add API services here
        // services.addCarter();

        return services;
    }
    public static WebApplication UseApiServices(this WebApplication app)
    {
        // Configure the HTTP request pipeline here
        // app.MapCarter();
        return app;
    }
}
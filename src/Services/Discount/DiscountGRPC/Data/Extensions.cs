using Microsoft.EntityFrameworkCore;

namespace DiscountGRPC.Data;
// Ensures database is ready before the service handles requests.
static class Extensions
{
    public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<DiscountContext>();
        dbContext.Database.MigrateAsync();

        return app;
    }
}
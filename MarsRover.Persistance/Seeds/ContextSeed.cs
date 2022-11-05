using MarsRover.Domain.Constants;
using MarsRover.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MarsRover.Persistance.Seeds;

public static class ContextSeed
{
    public static IServiceCollection CheckAndSeedData(this IServiceCollection services, bool isStartedWithMain)
    {
        if (!isStartedWithMain)
        {
            return services;
        }

        var serviceProvider = services.BuildServiceProvider();

        using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            // Migrating application db context and seeding configuration data
            var applicationDbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();

            // Since we are using in memory database, that's why we don't need to add migrations
            //applicationDbContext?.Database.Migrate();

            SeedData(applicationDbContext);
        }
        return services;
    }

    private static void SeedData(ApplicationDbContext context)
    {
        // TODO - Add seeding logic here
        #region SeedData
        
        #endregion
        //context.SaveChanges();
    }
}
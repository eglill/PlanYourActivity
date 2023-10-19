#pragma warning disable 1591

using App.DAL.EF;
using App.DAL.EF.Seeding;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace WebApp;

public class AppDataHelper
{
    public static void SetupAppData(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
    {
        using var serviceScope = app.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();
        using var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

        if (context == null)
        {
            throw new ApplicationException("Problem in services. Can't initialize DB Context");
        }

        using var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();
        using var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<AppRole>>();
    
        if (userManager == null || roleManager == null)
        {
            throw new ApplicationException("Problem in services. Can't initialize UserManager or RoleManager");
        }
    
        var logger = serviceScope.ServiceProvider.GetService<ILogger<IApplicationBuilder>>();
        if (logger == null)
        {
            throw new ApplicationException("Problem in services. Can't initialize logger");
        }

        if (context.Database.ProviderName!.Contains("InMemory"))
        {
            if (configuration.GetValue<bool>("DataInitialization:SeedIdentity"))
            {
                logger.LogInformation("Seeding identity");
                AppDataInit.SeedIdentity(context, userManager, roleManager);
            }
            
            if (configuration.GetValue<bool>("DataInitialization:SeedData"))
            {
                logger.LogInformation("Seed app data");
                AppDataInit.SeedAppData(context);
            }
            return;
        }

        // TODO: wait for db connection


        if (configuration.GetValue<bool>("DataInitialization:DropDatabase"))
        {
            logger.LogWarning("Dropping database");
            AppDataInit.DropDatabase(context);
        }

        if (configuration.GetValue<bool>("DataInitialization:MigrateDatabase"))
        {
            logger.LogInformation("Migrating database");
            AppDataInit.MigrateDatabase(context);
        }

        if (configuration.GetValue<bool>("DataInitialization:SeedIdentity"))
        {
            logger.LogInformation("Seeding identity");
            AppDataInit.SeedIdentity(context, userManager, roleManager);
        }

        if (configuration.GetValue<bool>("DataInitialization:SeedData"))
        {
            logger.LogInformation("Seed app data");
            AppDataInit.SeedAppData(context);
        }
    }
}
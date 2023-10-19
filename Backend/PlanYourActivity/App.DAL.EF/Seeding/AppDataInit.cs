using System.Security.Claims;
using App.Domain;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Seeding;

public class AppDataInit
{
    private static Guid adminId = Guid.Parse("bc7458ac-cbb0-4ecd-be79-d5abf19f8c77");

    public static void MigrateDatabase(ApplicationDbContext context)
    {
        context.Database.Migrate();
    }

    public static void DropDatabase(ApplicationDbContext context)
    {
        context.Database.EnsureDeleted();
    }

    public static void SeedIdentity(ApplicationDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        if (!context.Genders.Any())
        {
            SeedAppDataGender(context);
            context.SaveChanges();
        }
        
        var gender = context.Genders.First();

        (Guid id, string email, string pwd) userData = (adminId, "admin@admin.com", "Admin.1");
        var user = userManager.FindByEmailAsync(userData.email).Result;
        if (user == null)
        {
            user = new AppUser()
            {
                Id = userData.id,
                Email = userData.email,
                UserName = userData.email,
                FirstName = "Admin",
                LastName = "Admin",
                BirthDate = new DateOnly(1999, 1, 1),
                GenderId = gender.Id,
                EmailConfirmed = true,
            };
            var result = userManager.CreateAsync(user, userData.pwd).Result;
            result = userManager.AddClaimAsync(user, new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname", user.FirstName))
                .Result;
            result = userManager.AddClaimAsync(user, new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname", user.LastName))
                .Result;
            if (!result.Succeeded)
            {
                throw new ApplicationException($"Cannot seed users, {result}");
            }
        }
    }

    public static void SeedAppData(ApplicationDbContext context)
    {
        SeedAppDataGender(context);
        SeedAppDataColour(context);
        SeedAppDataCountry(context);
        SeedAppDataActivity(context);
        context.SaveChanges();
    }
    
    private static void SeedAppDataGender(ApplicationDbContext context)
    {
        if (context.Genders.Any()) return;

        context.Genders.Add(new Gender
            {
                Name = "Male",
            }
        );
        context.Genders.Add(new Gender
            {
                Name = "Female",
            }
        );
    }
    private static void SeedAppDataColour(ApplicationDbContext context)
    {
        if (context.Colours.Any()) return;

        var colours = new List<string>(new []
        {
            "#0000ff", "#9000ff", "#ff00cc", "#ff0000", "#ff8000", "#ffff00", "#00ff00", "#00ffff"
        });

        foreach (var colour in colours)
        {
            context.Colours.Add(new Colour
                {
                    Hex = colour
                }
            );
        }
    }
    
    private static void SeedAppDataCountry(ApplicationDbContext context)
    {
        if (context.Countries.Any()) return;

        var countries = new List<string>(new []
        {
            "Estonia", "Latvia", "Lithuania"
        });

        foreach (var country in countries)
        {
            context.Countries.Add(new Country
                {
                    Name = country
                }
            );
        }
    }

    private static void SeedAppDataActivity(ApplicationDbContext context)
    {
        if (context.Activities.Any()) return;

        var activities = new List<string>(new []
        {
            "Paintball", "Basketball", "Football", "Exploration", "Free time"
        });

        foreach (var activity in activities)
        {
            context.Activities.Add(new Activity
                {
                    Name = activity
                }
            );
        }
    }
}
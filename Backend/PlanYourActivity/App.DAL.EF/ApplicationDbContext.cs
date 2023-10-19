using App.Domain;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF;

public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public DbSet<Activity> Activities { get; set; } = default!;
    public DbSet<BlockedUser> BlockedUsers { get; set; } = default!;
    public DbSet<Colour> Colours { get; set; } = default!;
    public DbSet<Conversation> Conversations { get; set; } = default!;
    public DbSet<Country> Countries { get; set; } = default!;
    public DbSet<Event> Events { get; set; } = default!;
    public DbSet<UserEvent> UserEvents { get; set; } = default!;
    public DbSet<EventInGroup> EventInGroups { get; set; } = default!;
    public DbSet<Gender> Genders { get; set; } = default!;
    public DbSet<Group> Groups { get; set; } = default!;
    public DbSet<InviteToGroup> InviteToGroups { get; set; } = default!;
    public DbSet<Location> Locations { get; set; } = default!;
    public DbSet<Message> Messages { get; set; } = default!;
    public DbSet<PollOption> PollOptions { get; set; } = default!;
    public DbSet<PollQuestion> PollQuestions { get; set; } = default!;
    public DbSet<UserInGroup> UserInGroups { get; set; } = default!;
    public DbSet<UserOptionChoice> UserOptionChoices { get; set; } = default!;
    
    public DbSet<AppRefreshToken> AppRefreshTokens { get; set; } = default!;
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        // let the initial stuff run
        base.OnModelCreating(builder);

        // disable cascade delete
        foreach (var foreignKey in builder.Model
                     .GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }

}

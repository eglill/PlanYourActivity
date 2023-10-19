using App.BLL.DTO;
using App.BLL.Services;
using App.DAL.EF;
using App.Domain.Identity;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApp.ApiControllers;
using Xunit.Abstractions;

namespace Tests.Unit;

public class GroupServiceUnitTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ApplicationDbContext _ctx;
    private readonly IMapper _DALBLLMapper;
    private readonly IMapper _DALDomainMapper;
    private readonly GroupService _groupService;
    private readonly AppUOW _uow;

    
    public GroupServiceUnitTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        
        // configuring IMapper manually, Dependency Injection cannot reach here
        var myProfile = new App.BLL.AutoMapperConfig();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
        _DALBLLMapper = new Mapper(configuration);

        var myProfile1 = new App.DAL.EF.AutoMapperConfig();
        var configuration1 = new MapperConfiguration(cfg => cfg.AddProfile(myProfile1));
        _DALDomainMapper = new Mapper(configuration1);
        
        // set up mock database - inMemory
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        
        // use random guid as db instance id
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        _ctx = new ApplicationDbContext(optionsBuilder.Options);
        
        // reset db
        _ctx.Database.EnsureDeleted();
        _ctx.Database.EnsureCreated();
        _uow = new AppUOW(_ctx, _DALDomainMapper);
        
        using var logFactory = LoggerFactory.Create(builder => builder.AddConsole());
        var logger = logFactory.CreateLogger<GroupController>();
        
        _groupService = new GroupService(_uow, _DALBLLMapper);
    }
    
    [Fact(DisplayName = "Get - AllAsync")]
    public async Task Test_Group_AllAsync()
    {
        // Arrange
        // Seed the data
        await SeedUsersAsync();
        await SeedGroupsAsync();
        await SeedUserInGroupsAsync();
        
        var result = (await _groupService.AllAsync(new Guid("6a805b7a-31c4-4643-a756-1194533b882b")))
            .Select(c => c).ToList();
        
        List<Group> expected = new()
        {
            new Group
            {
                Id = new Guid("6a805b7a-31c4-4643-a756-1194533b882c"),
                Name = "Have fun 2",
                Description = "Let's have fun together 2",
                MaxParticipants = 10,
                Participants = 1
            }
        };
        
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(expected.Select(x => x.Id), result.Select(x => x.Id));
        Assert.Equal(expected.Select(x => x.Name), result.Select(x => x.Name));
    }

    [Fact(DisplayName = "Get - FindGroupAsync")]
    public async Task Test_Group_FindGroupAsync()
    {
        // Seed Group
        await SeedUsersAsync();
        await SeedGroupsAsync();

        var result = await _groupService.FindGroupAsync(
            new Guid("6a805b7a-31c4-4643-a756-1194533b881c"),
            new Guid("6a805b7a-31c4-4643-a756-1194533b881b")
        );
        
        // If user not in group then null
        Assert.Null(result);
        
        // Seed user in group
        await SeedUserInGroupsAsync();

        result = await _groupService.FindGroupAsync(
            new Guid("6a805b7a-31c4-4643-a756-1194533b881c"),
            new Guid("6a805b7a-31c4-4643-a756-1194533b881b")
        );
        
        // If user in group then not null
        Assert.NotNull(result);
        Assert.IsType<Guid>(result.Id);
        Assert.Equal("Have fun" , result.Name);
        Assert.Equal("Male" , result.GroupGender);
    }

    [Fact(DisplayName = "Get - UserGroups")]
    public async Task Test_Group_UserGroups()
    {
        // Arrange
        // Seed the data
        await SeedUsersAsync();
        await SeedGroupsAsync();
        await SeedUserInGroupsAsync();

        var result = (await _groupService.UserGroups(new Guid("6a805b7a-31c4-4643-a756-1194533b881b")))
            .Select(c => c).ToList();;
        
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

    [Fact(DisplayName = "Post - JoinGroup")]
    public async Task Test_Group_JoinGroup()
    {
        // Arrange
        // Seed the data
        await SeedUsersAsync();
        await SeedGroupsAsync();
        await SeedUserInGroupsAsync();

        var result1 = await _groupService.JoinGroup(
            new Guid("6a805b7a-31c4-4643-a756-1194533b881c"),
            new Guid("6a805b7a-31c4-4643-a756-1194533b882b"));
        
        var result2 = await _groupService.JoinGroup(
            new Guid("6a805b7a-31c4-4643-a756-1194533b882c"),
            new Guid("6a805b7a-31c4-4643-a756-1194533b882b"));

        await _ctx.SaveChangesAsync();
        var joined = _ctx.UserInGroups.Count();
        
        Assert.False(result1);
        Assert.True(result2);
        Assert.Equal(3, joined);
    }

    [Fact(DisplayName = "Post - KickUser")]
    public async Task Test_Group_KickUser()
    {
        // Arrange
        // Seed the data
        await SeedUsersAsync();
        await SeedGroupsAsync();
        await SeedUserInGroupsAsync();
        await SeedUserInGroupsAsync2();
    
        var result = await _groupService.KickUser(
            new Guid("6a805b7a-31c4-4643-a756-1194533b881c"),
            new Guid("6a805b7a-31c4-4643-a756-1194533b882b"),
            new Guid("6a805b7a-31c4-4643-a756-1194533b881b")
        );
        
        Assert.True(result);
        // Assert.Equal(3, joined);
    }
    
    [Fact(DisplayName = "Put - Update")]
    public async Task Test_Group_Update()
    {
        // Arrange
        // Seed the data
        await SeedUsersAsync();
        await SeedGroupsAsync();
        await SeedUserInGroupsAsync();
        
        var updateGroup = new AddGroup
        {
            Id = new Guid("6a805b7a-31c4-4643-a756-1194533b881c"),
            Name = "Be sad",
            Description = "Let's be sad together",
            MaxParticipants = 10,
            Private = false,
            JoiningLocked = false,
            GroupGenderId = new Guid("6a805b7a-31c4-4643-a756-1194533b881a"),
            AdminId = new Guid("6a805b7a-31c4-4643-a756-1194533b881b")
        };

        // Detach the existing entity with the same Id if it's being tracked
        var existingGroup = _ctx.Groups.FirstOrDefault(c => c.Id == updateGroup.Id);
        if (existingGroup != null)
        {
            _ctx.Entry(existingGroup).State = EntityState.Detached;
        }
        
        var result1 = await _groupService.Update(updateGroup, new Guid("6a805b7a-31c4-4643-a756-1194533b882b"));
        var result2 = await _groupService.Update(updateGroup, new Guid("6a805b7a-31c4-4643-a756-1194533b881b"));
        
        Assert.False(result1);
        Assert.True(result2);
    }
    
    [Fact(DisplayName = "Post - Add")]
    public async Task Test_Group_Add()
    {
        // Arrange
        // Seed the data
        await SeedUsersAsync();
        
        var group = new AddGroup()
        {
            Name = "Have fun",
            Description = "Let's have fun together",
            MaxParticipants = 10,
            Private = false,
            JoiningLocked = false,
            GroupGenderId = new Guid("6a805b7a-31c4-4643-a756-1194533b881a"),
            AdminId = new Guid("6a805b7a-31c4-4643-a756-1194533b881b")
        };

        var result = _groupService.Add(group);
        
        Assert.NotNull(result);
        Assert.IsType<Guid>(result.Id);
        Assert.Equal("Have fun" , result.Name);
        Assert.Equal("Male" , result.GroupGender);
    }
    
    [Fact(DisplayName = "Get - UsersInGroup")]
    public async Task Test_Group_UsersInGroup()
    {
        // Arrange
        // Seed the data
        await SeedUsersAsync();
        await SeedGroupsAsync();

        var result1 = await _groupService.UsersInGroup(
            new Guid("6a805b7a-31c4-4643-a756-1194533b881c"),
            new Guid("6a805b7a-31c4-4643-a756-1194533b881b"));
        
        await SeedUserInGroupsAsync();
        await SeedUserInGroupsAsync2();
        var result2 = await _groupService.UsersInGroup(
            new Guid("6a805b7a-31c4-4643-a756-1194533b881c"),
            new Guid("6a805b7a-31c4-4643-a756-1194533b881b"));
        
        Assert.Null(result1);
        Assert.NotNull(result2);
        Assert.Equal(2, result2.Select(c => c).ToList().Count);
    }
    
    [Fact(DisplayName = "Delete - Delete")]
    public async Task Test_Group_Delete()
    {
        // Arrange
        // Seed the data
        await SeedUsersAsync();
        await SeedGroupsAsync();
        await SeedUserInGroupsAsync();
        await SeedUserInGroupsAsync2();
        
        // Detach the existing entity with the same Id if it's being tracked
        var existingGroup = _ctx.Groups.FirstOrDefault(
            c => c.Id == new Guid("6a805b7a-31c4-4643-a756-1194533b881c"));
        if (existingGroup != null)
        {
            _ctx.Entry(existingGroup).State = EntityState.Detached;
        }

        var result = await _groupService.Delete(
            new Guid("6a805b7a-31c4-4643-a756-1194533b881c"),
            new Guid("6a805b7a-31c4-4643-a756-1194533b881b"));

        await _ctx.SaveChangesAsync();

        var groups = _ctx.Groups.Count();
        var usersInGroup = _ctx.UserInGroups.Count();
        
        Assert.True(result);
        Assert.Equal(1, groups);
        Assert.Equal(2, usersInGroup);
    }

    private async Task SeedGendersAsync()
    {
        _ctx.Genders.Add(new App.Domain.Gender
            {
                Id = new Guid("6a805b7a-31c4-4643-a756-1194533b881a"),
                Name = "Male"
            }
        );
        
        _ctx.Genders.Add(new App.Domain.Gender
            {
                Id = new Guid("6a805b7a-31c4-4643-a756-1194533b882a"),
                Name = "Female"
            }
        );
        await _ctx.SaveChangesAsync();
    }

    private async Task SeedUsersAsync()
    {
        await SeedGendersAsync();
        
        _ctx.Users.Add(new AppUser()
        {
            Id = new Guid("6a805b7a-31c4-4643-a756-1194533b881b"),
            Email = "user@user.com",
            UserName = "TestUsername",
            FirstName = "Cool",
            LastName = "User",
            BirthDate = new DateOnly(1999, 1, 1),
            GenderId = new Guid("6a805b7a-31c4-4643-a756-1194533b881a"),
            EmailConfirmed = true,
            PasswordHash = "lkajdsgfapjhrdgsosofashgwaeifsdhj"
        });
        
        _ctx.Users.Add(new AppUser()
        {
            Id = new Guid("6a805b7a-31c4-4643-a756-1194533b882b"),
            Email = "user2@user.com",
            UserName = "TestUsername2",
            FirstName = "Cool2",
            LastName = "User2",
            BirthDate = new DateOnly(2010, 1, 1),
            GenderId = new Guid("6a805b7a-31c4-4643-a756-1194533b882a"),
            EmailConfirmed = true,
            PasswordHash = "lkajdsgfapjhrdgsosofashgwaeifsdhj"
        });

        await _ctx.SaveChangesAsync();
    }

    private async Task SeedGroupsAsync()
    {
        _ctx.Groups.Add(new App.Domain.Group
        {
            Id = new Guid("6a805b7a-31c4-4643-a756-1194533b881c"),
            Name = "Have fun",
            Description = "Let's have fun together",
            MaxParticipants = 10,
            CreatedAt = DateTime.UtcNow,
            Private = false,
            JoiningLocked = false,
            GroupGenderId = new Guid("6a805b7a-31c4-4643-a756-1194533b881a"),
            AdminId = new Guid("6a805b7a-31c4-4643-a756-1194533b881b")
        });
        
        _ctx.Groups.Add(new App.Domain.Group
        {
            Id = new Guid("6a805b7a-31c4-4643-a756-1194533b882c"),
            Name = "Have fun 2",
            Description = "Let's have fun together 2",
            MaxParticipants = 10,
            CreatedAt = DateTime.UtcNow,
            Private = false,
            JoiningLocked = false,
            AdminId = new Guid("6a805b7a-31c4-4643-a756-1194533b881b")
        });
        
        await _ctx.SaveChangesAsync();
    }

    private async Task SeedUserInGroupsAsync()
    {
        _ctx.UserInGroups.Add(new App.Domain.UserInGroup
        {
            Id = new Guid("6a805b7a-31c4-4643-a756-1194533b881d"),
            JoinedAt = DateTime.UtcNow,
            GroupId = new Guid("6a805b7a-31c4-4643-a756-1194533b881c"),
            AppUserId = new Guid("6a805b7a-31c4-4643-a756-1194533b881b"),
        });
        
        _ctx.UserInGroups.Add(new App.Domain.UserInGroup
        {
            Id = new Guid("6a805b7a-31c4-4643-a756-1194533b882d"),
            JoinedAt = DateTime.UtcNow,
            GroupId = new Guid("6a805b7a-31c4-4643-a756-1194533b882c"),
            AppUserId = new Guid("6a805b7a-31c4-4643-a756-1194533b881b"),
        });
        
        await _ctx.SaveChangesAsync();
    }
    
    private async Task SeedUserInGroupsAsync2()
    {
        _ctx.UserInGroups.Add(new App.Domain.UserInGroup
        {
            Id = new Guid("6a805b7a-31c4-4643-a756-1194533b883d"),
            JoinedAt = DateTime.UtcNow,
            GroupId = new Guid("6a805b7a-31c4-4643-a756-1194533b881c"),
            AppUserId = new Guid("6a805b7a-31c4-4643-a756-1194533b882b"),
        });
        
        _ctx.UserInGroups.Add(new App.Domain.UserInGroup
        {
            Id = new Guid("6a805b7a-31c4-4643-a756-1194533b884d"),
            JoinedAt = DateTime.UtcNow,
            GroupId = new Guid("6a805b7a-31c4-4643-a756-1194533b882c"),
            AppUserId = new Guid("6a805b7a-31c4-4643-a756-1194533b882b"),
        });
        
        await _ctx.SaveChangesAsync();
    }
}
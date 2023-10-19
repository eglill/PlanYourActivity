using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Public.DTO.v1;
using Public.DTO.v1.Identity;
using Tests.Helpers;
using WebApp;
using Xunit.Abstractions;

namespace Tests.Integration.api;

public class HappyFlowIntegrationApiTest : IClassFixture<CustomWebAppFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebAppFactory<Program> _factory;
    private readonly ITestOutputHelper _testOutputHelper;
    
    public HappyFlowIntegrationApiTest(CustomWebAppFactory<Program> factory, ITestOutputHelper testOutputHelper)
    {
        _factory = factory;
        _testOutputHelper = testOutputHelper;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }
    
    [Fact(DisplayName = "Happy flow integration")]
    public async void StartHappyFlow()
    {
        await Api_Register();
    }
    
    public async Task Api_Register()
    {
        // Arrange
        const string urlGenders = "/api/v1/gender/";
        const string urlRegister = "/api/v1/identity/account/register";
        const string email = "test@user.ee";
        const string firstname = "TestFirst";
        const string lastname = "TestLast";
        const string password = "Foo.bar1";
        
        var genderResponse = await _client.GetAsync(urlGenders);
        var genderContent = await genderResponse.Content.ReadAsStringAsync();
        var gender = JsonHelper.DeserializeWithWebDefaults<IEnumerable<Gender>>(genderContent)!.FirstOrDefault();
        
        var registerData = new Register
        {
            Email = email,
            Password = password,
            Firstname = firstname,
            Lastname = lastname,
            BirthDate = new DateOnly(1999, 1, 1),
            Gender = gender!.Id,
        };
        var data = JsonContent.Create(registerData);

        // Act
        var response = await _client.PostAsync(urlRegister, data);

        // Assert
        genderResponse.EnsureSuccessStatusCode();
        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal(200, (int)response.StatusCode);
        Assert.NotNull(data);
        
        _testOutputHelper.WriteLine("Register success!");
        await Api_Login();
    }
    
    public async Task Api_Login()
    {
        // ARRANGE
        var uri = "/api/v1/Identity/Account/Login";

        var req = new Public.DTO.v1.Identity.Login
        {
            Email = "admin@admin.com",
            Password = "Admin.1",
        };

        // ACT
        var getRegResponse = await _client.PostAsync(uri, JsonHelper.GetStringContent(req));
        var body2 = await getRegResponse.Content.ReadAsStringAsync();
        //data - Jwt info.
        var data = JsonHelper.DeserializeWithWebDefaults<Public.DTO.v1.Identity.JWTResponse>(body2);

        // ASSERT
        getRegResponse.EnsureSuccessStatusCode();
        Assert.Equal(200, (int)getRegResponse.StatusCode);

        var body = await getRegResponse.Content.ReadAsStringAsync();
        _testOutputHelper.WriteLine("Login admin success!");
        await Api_Create_Group(data!);
    }
    
    public async Task Api_Create_Group(JWTResponse jwtResponse)
    {
        // ARRANGE
        var url = "/api/v1/group/";

        var group = new AddGroup
        {
            Name = "Test Group",
            Description = "Test description",
            MaxParticipants = 10,
            Private = false,
            JoiningLocked = false
        };

        // ACT
        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse.JWT);
        request.Content = JsonHelper.GetStringContent(group);
        var createResponse = await _client.SendAsync(request);
        var response = await createResponse.Content.ReadAsStringAsync();
        var createdGroup = JsonHelper.DeserializeWithWebDefaults<Group>(response);

        // ASSERT
        createResponse.EnsureSuccessStatusCode();
        Assert.IsType<Guid>(createdGroup!.Id);
        Assert.Equal(201, (int)createResponse.StatusCode);
        _testOutputHelper.WriteLine("Create group success!");

        await Api_Create_Group_Event(jwtResponse, createdGroup);
    }
    
    public async Task Api_Create_Group_Event(JWTResponse jwtResponse, Group group)
    {
        // ARRANGE
        var urlAddEvent = "/api/v1/event/group/";
        var urlColour = "/api/v1/colour/";
        var urlActivity = "/api/v1/activity/";
        var urlCountry = "/api/v1/country/";
        
        var colourRequest = new HttpRequestMessage(HttpMethod.Get, urlColour);
        colourRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse.JWT);
        var colourResponse = await _client.SendAsync(colourRequest);
        var colourContent = await colourResponse.Content.ReadAsStringAsync();
        var colour = JsonHelper.DeserializeWithWebDefaults<IEnumerable<Colour>>(colourContent)!.FirstOrDefault();
        
        var activityRequest = new HttpRequestMessage(HttpMethod.Get, urlActivity);
        activityRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse.JWT);
        var activityResponse = await _client.SendAsync(activityRequest);
        var activityContent = await activityResponse.Content.ReadAsStringAsync();
        var activity = JsonHelper.DeserializeWithWebDefaults<IEnumerable<Activity>>(activityContent)!.FirstOrDefault();
        
        var countryRequest = new HttpRequestMessage(HttpMethod.Get, urlCountry);
        countryRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse.JWT);
        var countryResponse = await _client.SendAsync(countryRequest);
        var countryContent = await countryResponse.Content.ReadAsStringAsync();
        var country = JsonHelper.DeserializeWithWebDefaults<IEnumerable<Country>>(countryContent)!.FirstOrDefault();

        var groupEvent = new AddGroupEvent
        {
            Name = "Test event",
            StartsAt = DateTime.UtcNow.AddDays(1),
            EndsAt = DateTime.UtcNow.AddDays(1).AddHours(1),
            ColourId = colour!.Id,
            Location = new LocationAdd
            {
                Address = "Test address",
                Description = "Test description",
                CountryId = country!.Id
            },
            ActivityId = activity!.Id,
            GroupId = group.Id
        };

        // ACT
        var request = new HttpRequestMessage(HttpMethod.Post, urlAddEvent);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse.JWT);
        request.Content = JsonHelper.GetStringContent(groupEvent);
        var createResponse = await _client.SendAsync(request);
        var response = await createResponse.Content.ReadAsStringAsync();
        var createdGroupEvent = JsonHelper.DeserializeWithWebDefaults<GroupEvent>(response);

        // ASSERT
        colourResponse.EnsureSuccessStatusCode();
        activityResponse.EnsureSuccessStatusCode();
        countryResponse.EnsureSuccessStatusCode();
        createResponse.EnsureSuccessStatusCode();
        Assert.IsType<Guid>(createdGroupEvent!.Id);
        Assert.Equal(201, (int) createResponse.StatusCode);
        
        _testOutputHelper.WriteLine("Create group event success!");

        await Api_Get_Group_Events(jwtResponse, group);
    }
    
    public async Task Api_Get_Group_Events(JWTResponse jwtResponse, Group group)
    {
        // ARRANGE
        var urlGroupEvents = "/api/v1/event/group/all/" + group.Id;

        // ACT
        var request = new HttpRequestMessage(HttpMethod.Get, urlGroupEvents);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse.JWT);
        var getResponse = await _client.SendAsync(request);
        var response = await getResponse.Content.ReadAsStringAsync();
        var groupEvents = JsonHelper.DeserializeWithWebDefaults<IEnumerable<GroupEvent>>(response)!.ToList();
        var groupEvent = groupEvents.FirstOrDefault();

        // ASSERT
        getResponse.EnsureSuccessStatusCode();
        Assert.IsType<Guid>(groupEvent!.Id);
        Assert.Equal(200, (int) getResponse.StatusCode);
        _testOutputHelper.WriteLine("Get group events success!");

        
        await Api_Add_Event_To_Personal_Events(jwtResponse, groupEvent, group, groupEvents);
    }
    
    public async Task Api_Add_Event_To_Personal_Events(JWTResponse jwtResponse, GroupEvent groupEvent, Group group, List<GroupEvent> groupEvents)
    {
        // ARRANGE
        var urlAddToPersonal = "/api/v1/event/user/" + groupEvent.Id;

        // ACT
        var request = new HttpRequestMessage(HttpMethod.Post, urlAddToPersonal);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse.JWT);
        var postResponse = await _client.SendAsync(request);

        // ASSERT
        postResponse.EnsureSuccessStatusCode();
        Assert.Equal(200, (int) postResponse.StatusCode);
        _testOutputHelper.WriteLine("Add event to personal events success!");

        await Api_Get_Personal_Events(jwtResponse, group, groupEvents);
    }
    
    public async Task Api_Get_Personal_Events(JWTResponse jwtResponse, Group group, List<GroupEvent> groupEvents)
    {
        // ARRANGE
        var urlUserEvents = "/api/v1/event/user/";

        // ACT
        var request = new HttpRequestMessage(HttpMethod.Get, urlUserEvents);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse.JWT);
        var getResponse = await _client.SendAsync(request);
        var response = await getResponse.Content.ReadAsStringAsync();
        var userEvents = JsonHelper.DeserializeWithWebDefaults<IEnumerable<UserEvent>>(response)!.ToList();
        var userEvent = userEvents!.FirstOrDefault();

        // ASSERT
        getResponse.EnsureSuccessStatusCode();
        Assert.IsType<Guid>(userEvent!.Id);
        Assert.Equal(200, (int) getResponse.StatusCode);
        _testOutputHelper.WriteLine("Get personal events success!");

        await Api_Delete_Group(jwtResponse, group, userEvents, groupEvents);
    }
    
    public async Task Api_Delete_Group(JWTResponse jwtResponse, Group group, List<UserEvent> userEvents, List<GroupEvent> groupEvents)
    {
        // ARRANGE
        var deleteUrl = "/api/v1/group/admin/" + group.Id;

        // ACT
        var request = new HttpRequestMessage(HttpMethod.Delete, deleteUrl);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse.JWT);
        var createResponse = await _client.SendAsync(request);

        // ASSERT
        createResponse.EnsureSuccessStatusCode();
        Assert.Equal(204, (int)createResponse.StatusCode);
        _testOutputHelper.WriteLine("Delete event success!");

        await Api_Check_If_Group_Event_Deleted(jwtResponse, group, userEvents, groupEvents);
    }
    
    public async Task Api_Check_If_Group_Event_Deleted(JWTResponse jwtResponse, Group group, List<UserEvent> userEvents, List<GroupEvent> previousGroupEvents)
    {
        // ARRANGE
        var urlGroupEvents = "/api/v1/event/group/all/" + group.Id;

        // ACT
        var request = new HttpRequestMessage(HttpMethod.Get, urlGroupEvents);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse.JWT);
        var getResponse = await _client.SendAsync(request);
        
        // ASSERT
        Assert.Equal(400, (int) getResponse.StatusCode);
        _testOutputHelper.WriteLine("Group event deleted success!");

        await Api_Check_If_User_Event_Deleted(jwtResponse, userEvents);
    }
    
    public async Task Api_Check_If_User_Event_Deleted(JWTResponse jwtResponse, List<UserEvent> previousUserEvents)
    {
        // ARRANGE
        var urlUserEvents = "/api/v1/event/user/";

        // ACT
        var request = new HttpRequestMessage(HttpMethod.Get, urlUserEvents);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse.JWT);
        var getResponse = await _client.SendAsync(request);
        var response = await getResponse.Content.ReadAsStringAsync();
        var userEvents = JsonHelper.DeserializeWithWebDefaults<IEnumerable<UserEvent>>(response)!.ToList();

        // ASSERT
        getResponse.EnsureSuccessStatusCode();
        Assert.Equal(200, (int) getResponse.StatusCode);
        Assert.Equal(previousUserEvents.Count - 1, userEvents.Count);
        _testOutputHelper.WriteLine("User event deleted success!");
    }
}
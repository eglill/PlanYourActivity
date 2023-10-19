using App.BLL.DTO;
using AutoMapper;

namespace Public.DTO;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<Gender, v1.Gender>().ReverseMap();
        CreateMap<EventFilter, v1.EventFilter>().ReverseMap();
        CreateMap<GroupEvent, v1.GroupEvent>().ReverseMap();
        CreateMap<UserEvent, v1.UserEvent>().ReverseMap();
        CreateMap<v1.AddGroupEvent, AddGroupEvent>().ReverseMap();
        CreateMap<v1.AddUserEvent, AddUserEvent>().ReverseMap();
        CreateMap<Location, v1.Location>().ReverseMap();
        CreateMap<LocationAdd, v1.LocationAdd>().ReverseMap();
        CreateMap<Group, v1.Group>().ReverseMap();
        CreateMap<GroupWithData, v1.GroupWithData>().ReverseMap();
        CreateMap<AddGroup, v1.AddGroup>().ReverseMap();
        CreateMap<App.BLL.DTO.Identity.AppUser, v1.Identity.AppUser>().ReverseMap();
        CreateMap<Conversation, v1.Conversation>().ReverseMap();
        CreateMap<Message, v1.Message>().ReverseMap();
        CreateMap<AddMessage, v1.AddMessage>().ReverseMap();
        CreateMap<Country, v1.Country>().ReverseMap();
        CreateMap<Colour, v1.Colour>().ReverseMap();
        CreateMap<Activity, v1.Activity>().ReverseMap();
    }
}
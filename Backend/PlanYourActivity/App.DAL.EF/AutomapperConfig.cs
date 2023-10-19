using App.DAL.DTO;
using App.DAL.DTO.identity;
using AutoMapper;

namespace App.DAL.EF;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<Domain.Identity.AppUser, AppUser>().ReverseMap();
        CreateMap<Domain.Activity, Activity>().ReverseMap();
        CreateMap<Domain.BlockedUser, BlockedUser>().ReverseMap();
        CreateMap<Domain.Colour, Colour>().ReverseMap();
        CreateMap<Domain.Conversation, Conversation>().ReverseMap();
        CreateMap<Domain.Country, Country>().ReverseMap();
        CreateMap<Domain.Event, Event>().ReverseMap();
        CreateMap<Domain.EventInGroup, EventInGroup>().ReverseMap();
        CreateMap<Domain.FavoriteUser, FavoriteUser>().ReverseMap();
        CreateMap<Domain.Gender, Gender>().ReverseMap();
        CreateMap<Domain.Group, Group>().ReverseMap();
        CreateMap<Domain.InviteToGroup, InviteToGroup>().ReverseMap();
        CreateMap<Domain.Location, Location>().ReverseMap();
        CreateMap<Domain.Message, Message>().ReverseMap();
        CreateMap<Domain.PollOption, PollOption>().ReverseMap();
        CreateMap<Domain.PollQuestion, PollQuestion>().ReverseMap();
        CreateMap<Domain.UserEvent, UserEvent>().ReverseMap();
        CreateMap<Domain.UserInGroup, UserInGroup>().ReverseMap();
        CreateMap<Domain.UserOptionChoice, UserOptionChoice>().ReverseMap();
    }
}
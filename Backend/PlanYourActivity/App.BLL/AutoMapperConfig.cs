using App.BLL.DTO;
using App.BLL.DTO.Identity;
using AutoMapper;

namespace App.BLL;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<DAL.DTO.identity.AppUser, AppUser>().ReverseMap();
        CreateMap<DAL.DTO.Gender, Gender>().ReverseMap();
        CreateMap<DAL.DTO.Country, Country>().ReverseMap();
        CreateMap<DAL.DTO.Colour, Colour>().ReverseMap();
        CreateMap<DAL.DTO.Activity, Activity>().ReverseMap();
        CreateMap<EventFilter, DAL.DTO.EventFilter>().ReverseMap();
        CreateMap<AddGroupEvent, DAL.DTO.Event>();
        CreateMap<LocationAdd, DAL.DTO.Location>().ReverseMap();
        CreateMap<BlockedUser, DAL.DTO.BlockedUser>().ReverseMap();
        CreateMap<AddGroup, DAL.DTO.Group>();
        CreateMap<DAL.DTO.Conversation, Conversation>();

        CreateMap<DAL.DTO.Location, Location>()
            .ForMember(dest => dest.Country,
                options => options
                    .MapFrom(src => src.Country!.Name));
        
        CreateMap<AddUserEvent, DAL.DTO.Event>()
            .ForMember(dest => dest.CreatorId,
                options => options
                    .MapFrom(src => src.AppUserId));
        
        CreateMap<DAL.DTO.Event, GroupEvent>()
            .ForMember(dest => dest.Colour,
                options => options
                    .MapFrom(src => src.Colour!.Hex))
            .ForMember(dest => dest.Activity,
                options => options
                    .MapFrom(src => src.Activity!.Name));
        
        CreateMap<DAL.DTO.Event, UserEvent>()
            .ForMember(dest => dest.Colour,
                options => options
                    .MapFrom(src => src.Colour!.Hex))
            .ForMember(dest => dest.Activity,
                options => options
                    .MapFrom(src => src.Activity!.Name));

        CreateMap<DAL.DTO.Group, Group>()
            .ForMember(dest => dest.Participants,
                options => options
                    .MapFrom(src => src.UsersInGroup!.Count))
            .ForMember(dest => dest.GroupGender,
                options => options
                    .MapFrom(src => src.GroupGender!.Name));
        
        CreateMap<DAL.DTO.Group, GroupWithData>()
            .ForMember(dest => dest.Participants,
                options => options
                    .MapFrom(src => src.UsersInGroup!.Count))
            .ForMember(dest => dest.Admin,
                options => options
                    .MapFrom(src => $"{src.Admin!.FirstName} {src.Admin!.LastName}"))
            .ForMember(dest => dest.GroupGender,
                options => options
                    .MapFrom(src => src.GroupGender!.Name));

        CreateMap<DAL.DTO.Message, Message>()
            .ForMember(dest => dest.Creator,
                options => options
                    .MapFrom(src => $"{src.Creator!.FirstName} {src.Creator!.LastName}"));
    }
}
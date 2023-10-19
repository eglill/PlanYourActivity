using App.BLL.DTO;
using AutoMapper;
using Base.DAL;

namespace Public.DTO.Mappers;

public class LocationMapper : BaseMapper<Location, Public.DTO.v1.Location>
{
    public LocationMapper(IMapper mapper) : base(mapper)
    {
    }
}
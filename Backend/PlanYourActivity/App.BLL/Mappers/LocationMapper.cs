using App.BLL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class LocationMapper : BaseMapper<DAL.DTO.Location, Location>
{
    public LocationMapper(IMapper mapper) : base(mapper)
    {
    }
}
using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class LocationMapper : BaseMapper<Domain.Location, Location>
{
    public LocationMapper(IMapper mapper) : base(mapper)
    {
    }
}
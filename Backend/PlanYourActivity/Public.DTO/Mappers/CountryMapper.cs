using App.BLL.DTO;
using AutoMapper;
using Base.DAL;

namespace Public.DTO.Mappers;

public class CountryMapper : BaseMapper<Country, Public.DTO.v1.Country>
{
    public CountryMapper(IMapper mapper) : base(mapper)
    {
    }
}
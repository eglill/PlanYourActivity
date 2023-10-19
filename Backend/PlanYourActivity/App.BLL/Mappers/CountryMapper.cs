using App.BLL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class CountryMapper : BaseMapper<DAL.DTO.Country, Country>
{
    public CountryMapper(IMapper mapper) : base(mapper)
    {
    }
}
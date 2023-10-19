using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class CountryMapper : BaseMapper<Domain.Country, Country>
{
    public CountryMapper(IMapper mapper) : base(mapper)
    {
    }
}
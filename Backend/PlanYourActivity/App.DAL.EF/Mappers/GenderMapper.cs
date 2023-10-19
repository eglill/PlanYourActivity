using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class GenderMapper : BaseMapper<Domain.Gender, Gender>
{
    public GenderMapper(IMapper mapper) : base(mapper)
    {
    }
}
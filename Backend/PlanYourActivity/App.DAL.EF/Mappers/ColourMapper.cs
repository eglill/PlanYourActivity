using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class ColourMapper : BaseMapper<Domain.Colour, Colour>
{
    public ColourMapper(IMapper mapper) : base(mapper)
    {
    }
}
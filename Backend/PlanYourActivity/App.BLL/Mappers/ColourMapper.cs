using App.BLL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class ColourMapper : BaseMapper<DAL.DTO.Colour, Colour>
{
    public ColourMapper(IMapper mapper) : base(mapper)
    {
    }
}
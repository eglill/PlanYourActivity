using App.BLL.DTO;
using AutoMapper;
using Base.DAL;

namespace Public.DTO.Mappers;

public class ColourMapper : BaseMapper<Colour, Public.DTO.v1.Colour>
{
    public ColourMapper(IMapper mapper) : base(mapper)
    {
    }
}
using App.BLL.DTO;
using AutoMapper;
using Base.DAL;

namespace Public.DTO.Mappers;

public class GenderMapper : BaseMapper<Gender, Public.DTO.v1.Gender>
{
    public GenderMapper(IMapper mapper) : base(mapper)
    {
    }
}
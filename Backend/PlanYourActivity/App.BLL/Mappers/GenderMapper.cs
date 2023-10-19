using App.BLL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class GenderMapper : BaseMapper<DAL.DTO.Gender, Gender>
{
    public GenderMapper(IMapper mapper) : base(mapper)
    {
    }
}
using App.BLL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class ActivityMapper : BaseMapper<DAL.DTO.Activity, Activity>
{
    public ActivityMapper(IMapper mapper) : base(mapper)
    {
    }
}
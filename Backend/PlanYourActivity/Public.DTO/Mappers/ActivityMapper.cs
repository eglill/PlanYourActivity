using App.BLL.DTO;
using AutoMapper;
using Base.DAL;

namespace Public.DTO.Mappers;

public class ActivityMapper : BaseMapper<Activity, Public.DTO.v1.Activity>
{
    public ActivityMapper(IMapper mapper) : base(mapper)
    {
    }
}
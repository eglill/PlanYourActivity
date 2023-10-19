using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class ActivityMapper : BaseMapper<Domain.Activity, Activity>
{
    public ActivityMapper(IMapper mapper) : base(mapper)
    {
    }
}
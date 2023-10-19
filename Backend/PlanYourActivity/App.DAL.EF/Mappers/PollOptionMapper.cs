using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class PollOptionMapper : BaseMapper<Domain.PollOption, PollOption>
{
    public PollOptionMapper(IMapper mapper) : base(mapper)
    {
    }
}
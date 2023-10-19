using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class PollQuestionMapper : BaseMapper<Domain.PollQuestion, PollQuestion>
{
    public PollQuestionMapper(IMapper mapper) : base(mapper)
    {
    }
}
using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class FavoriteUserMapper : BaseMapper<Domain.FavoriteUser, FavoriteUser>
{
    public FavoriteUserMapper(IMapper mapper) : base(mapper)
    {
    }
}
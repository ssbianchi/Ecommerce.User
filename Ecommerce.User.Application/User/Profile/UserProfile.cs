using Ecommerce.User.Application.User.Dto;
using Ecommerce.User.Domain.Entity.Readonly.Dapper.User;

namespace Ecommerce.User.Application.User.Profile
{
    public class UserProfile : AutoMapper.Profile
    {
        public UserProfile()
        {
            CreateMap<Ecommerce.User.Domain.Entity.User.User, UserDto>().ReverseMap();
            CreateMap<DapperUser, UserDto>().ReverseMap();
        }
    }
}

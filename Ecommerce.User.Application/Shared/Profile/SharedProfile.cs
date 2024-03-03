using Ecommerce.User.Application.Shared.Dto;
using Ecommerce.User.Domain.Entity.Readonly.Dapper;

namespace Ecommerce.User.Application.Shared.Profile
{
    public class SharedProfile : AutoMapper.Profile
    {
        public SharedProfile()
        {
            CreateMap<DapperIdName, IdNameDto>();
        }
    }
}

using Ecommerce.User.Domain.Entity.User.Repository;
using Ecommerce.User.Repository.Context;

namespace Ecommerce.User.Repository.Repository
{
    public class UserRepository : UnitOfWork<Ecommerce.User.Domain.Entity.User.User>, IUserRepository
    {
        public UserRepository(EcommerceContext context) : base(context)
        {
        }
    }
}

using Ecommerce.User.Domain.Entity.Readonly.Dapper.User;

namespace Ecommerce.User.Domain.Entity.Readonly.Repository
{
    public interface IReadonlyRepository
    {
        #region User
        Task<IEnumerable<DapperUser>> GetAllUser();
        #endregion
    }
}

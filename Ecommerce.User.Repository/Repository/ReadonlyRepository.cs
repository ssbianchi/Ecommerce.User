using Ecommerce.User.Domain.Entity.Readonly.Dapper.User;
using Ecommerce.User.Domain.Entity.Readonly.Repository;
using Ecommerce.User.Repository.Context;
using Ecommerce.User.Repository.Repository.Options;
using Microsoft.Extensions.Options;

namespace Ecommerce.User.Repository.Repository
{
    public class ReadonlyRepository : UnitOfWorkQuery, IReadonlyRepository
    {
        public ReadonlyRepository(IOptions<ConnectionStringOptions> options) : base(options.Value.ConnectionString)
        {

        }

        #region User
        public async Task<IEnumerable<DapperUser>> GetAllUser()
        {
            var sql = @"
Select Id
     , Nome
     , Login
     , Password
     , Email
  From System_Users";

            //var result = await QueryAsync<DapperUser>(sql, new { Id = UserId });
            var result = await QueryAsync<DapperUser>(sql);
            return result;
        }
        #endregion
    }
}

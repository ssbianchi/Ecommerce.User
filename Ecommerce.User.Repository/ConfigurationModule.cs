using Ecommerce.User.Domain.Entity.Readonly.Repository;
using Ecommerce.User.Domain.Entity.User.Repository;
using Ecommerce.User.Repository.Context;
using Ecommerce.User.Repository.Repository;
using Ecommerce.User.Repository.Repository.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.User.Repository
{
    public static class ConfigurationModule
    {
        public static void RegisterRepository(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<EcommerceContext>(c =>
            {
                connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=Ecommerce_User;Trusted_Connection=True;";
                //connectionString = "Server=10.97.215.125,31433;Database=Ecommerce_User;Integrated Security=false;User ID=SA;Password=Sql2019isfast;Connection Timeout=30;TrustServerCertificate=True";
                c.UseSqlServer(connectionString);
            });

            //Use for Dapper
            services.Configure<ConnectionStringOptions>(c =>
            {
                c.ConnectionString = connectionString;
            });

            services.AddScoped<IReadonlyRepository, ReadonlyRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

        }

    }
}

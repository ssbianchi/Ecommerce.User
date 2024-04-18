using Ecommerce.User.Application.User;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.User.Application
{
    public static class ConfigurationModule
    {
        public static void RegisterApplication(this IServiceCollection service)
        {
            service.AddAutoMapper(typeof(ConfigurationModule).Assembly);

            service.AddScoped<IUserService, UserService>();
            service.AddHostedService<StartupBackgroundService>();
        }
    }
}
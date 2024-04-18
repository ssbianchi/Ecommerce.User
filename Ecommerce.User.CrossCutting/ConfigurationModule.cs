using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace Ecommerce.User.CrossCutting
{
    public static class ConfigurationModule
    {
        public static void RegisterCrossCutting(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddSingleton<StartupHealthCheck>();

            service.AddHealthChecks()
                            .AddCheck<LivenessHealthCheck>("Liveness")
                            .AddCheck<StartupHealthCheck>("Readiness");

            //service.Configure<ConsultaCEPWebAPIOptions>(c =>
            //{
            //    c.BaseUrl = configuration["ConsultaCEPWebAPI:BaseUrl"];
            //});

            //service.AddScoped<IConsultaCEPWebAPI, ConsultaCEPWebAPI>();
        }
    }
}

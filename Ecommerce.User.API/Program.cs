using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Ecommerce.User.API.Filter;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Ecommerce.User.Repository;
using Ecommerce.User.Application;
using Ecommerce.User.CrossCutting;
using Ecommerce.User.Application.User;

var builder = WebApplication.CreateBuilder(args);
var builderConfig = builder.Configuration;

// Add Key Vaults from Azure to appsettings configuration
// Add an ActionFilter which takes care of a standarised Exception message.
builder.Services.AddControllers(c => c.Filters.Add<HttpExceptionFilter>())
                .ConfigureApiBehaviorOptions(s => s.SuppressModelStateInvalidFilter = true);

// Suppress NULL fields in returned Json
builder.Services.AddMvc().AddJsonOptions(options =>
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "Ecommerce User API",
        Description = "An ASP.NET Core Web API to acess diverse services of the Ecommerce User.",
        Contact = new OpenApiContact { Name = "Ecommerce", Url = new Uri("https://www.ecommerce.com"), Email = "ecommerce@ecommerce.com" },
    });
});

// Register the layers config
builder.Services.RegisterRepository(builderConfig["EcommerceUserDatabase"]);
builder.Services.RegisterApplication();
builder.Services.RegisterCrossCutting(builderConfig);
//builder.Services.AddSingleton<StartupHealthCheck>();

//builder.Services.AddHealthChecks()
//                .AddCheck<LivenessHealthCheck>("Liveness")
//                .AddCheck<StartupHealthCheck>("Readiness");
var app = builder.Build();

app.MapHealthChecks("/healthz", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions()
{
    Predicate = healthCheck => healthCheck.Name == "Liveness"
});
app.MapHealthChecks("/ready", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions()
{
    Predicate = healthCheck => healthCheck.Name == "Readiness"
});

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
// }

app.UseAuthorization();

app.MapControllers();

app.Run();

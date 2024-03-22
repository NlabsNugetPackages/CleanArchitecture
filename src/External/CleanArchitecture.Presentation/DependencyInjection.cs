using CleanArchitecture.Application;
using CleanArchitecture.Application.Options;
using CleanArchitecture.Persistance;
using CleanArchitecture.Presentation.Middleware;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CleanArchitecture.Presentation;
public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddExceptionHandler<ExceptionMiddleware>();
        services.AddProblemDetails();

        services.Configure<Jwt>(configuration.GetSection("Jwt"));

        var serviceProvider = services.BuildServiceProvider();

        var jwtConfiguration = serviceProvider.GetRequiredService<IOptions<Jwt>>().Value;


        services.Configure<EmailOptions>(configuration.GetSection("Email"));

        services.AddApplication();
        services.AddPersistance(configuration);

        services.AddAuthentication().AddJwtBearer(cfr =>
        {
            cfr.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = jwtConfiguration.Issuer,
                ValidAudience = jwtConfiguration.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.SecretKey))
            };
        });

        services.AddCors(cfr =>
        {
            cfr.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.AllowAnyOrigin();
                policy.SetIsOriginAllowed(policy => true);
            });
        });


        services.AddTransient<ExceptionMiddleware>();

        return services;
    }
}
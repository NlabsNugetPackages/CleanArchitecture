using CleanArchitecture.Application;
using CleanArchitecture.Application.Options;
using CleanArchitecture.Persistance;
using CleanArchitecture.Presentation.Middleware;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Presentation;
public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
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

        services.AddExceptionHandler<ExceptionMiddleware>();
        services.AddProblemDetails();


        services.AddApplication(configuration);

        services.AddPersistance(configuration);

        services.AddSingleton<EmailOptions>();

        services.AddTransient<ExceptionMiddleware>();

        return services;
    }
}
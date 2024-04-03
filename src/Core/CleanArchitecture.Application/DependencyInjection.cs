using CleanArchitecture.Application.Behaviors;
using CleanArchitecture.Application.Options;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            //configuration.RegisterServicesFromAssemblies(
            //    typeof(DependencyInjection).Assembly,
            //    typeof(AppUser).Assembly);
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        var emailOptions = services.BuildServiceProvider().GetRequiredService<IOptions<EmailOptions>>().Value;

        services.AddAutoMapper(typeof(DependencyInjection).Assembly);


        services.Configure<EmailOptions>(configuration.GetSection("Email"));

        return services;
    }
}

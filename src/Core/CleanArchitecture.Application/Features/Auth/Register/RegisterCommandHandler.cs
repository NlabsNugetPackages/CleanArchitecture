using CleanArchitecture.Application.Events;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Application.Utilities;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Features.Auth.Register;
public sealed class RegisterCommandHandler(UserManager<AppUser> userManager, IMediator mediator) : IRequestHandler<RegisterCommand, Result<string>>
{
    public async Task<Result<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        Random? random = new();

        int emailConfirmCode = random.Next(111111, 999999);
        var isEmailConfirmCodeExists = true;

        while (isEmailConfirmCodeExists)
        {
            isEmailConfirmCodeExists = await userManager.Users.AnyAsync(p => p.EmailConfirmCode == emailConfirmCode, cancellationToken);

            if (isEmailConfirmCodeExists)
            {
                emailConfirmCode = random.Next(111111, 999999);
            }
        }

        if (request.RePassword != request.Password)
        {
            return Result<string>.Failure("Password and password repeat do not match.");
        }

        AppUser? user = new()
        {
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),
            Email = request.Email.ToLower().Trim(),
            UserName = request.UserName.ReplaceAllTurkishCharacters().ToLower().Trim(),
            EmailConfirmCode = emailConfirmCode,
        };

        var result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            return Result<string>.Failure("An error occurred while registering, please try again.");
        }

        var subject = "Kayıt Aktivasyon Emaili";

        //event üzerinden email gönderimi başarılı oldugu için buna gerek kalmadı bir alttaki event üzerinden gönderim saglanıyor artık
        //await mediator.Publish(EmailService.SendEmailAsync(user.Email, subject, user.EmailConfirmCode.ToString(), cancellationToken));

        await mediator.Publish(new AuthDomainEvent(user, subject, "", emailConfirmCode.ToString()));

        return Result<string>.Succeed("User created successfully");
    }
}

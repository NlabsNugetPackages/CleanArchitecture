using CleanArchitecture.Application.Events.Auth;
using CleanArchitecture.Application.Utilities;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Features.Auth.ForgotPasswordEmail;

internal sealed class ForgotPasswordEmailCommandHandler(UserManager<AppUser> userManager, IMediator mediator) : IRequestHandler<ForgotPasswordEmailCommand, Result<string>>
{
    public async Task<Result<string>> Handle(ForgotPasswordEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.Users.Where(p => p.UserName == request.UserNameOrEmail || p.Email == request.UserNameOrEmail).FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            return Result<string>.Failure( "User not found!");
        }

        Random? random = new();
        var isForgotPasswordCodeExists = true;
        var forgotPasswordCode = 0;

        while (isForgotPasswordCodeExists)
        {
            forgotPasswordCode = random.Next(1111111111, 999999999);
            isForgotPasswordCodeExists = await userManager.Users.AnyAsync(p => p.ForgotPasswordCode == forgotPasswordCode, cancellationToken);

            if (isForgotPasswordCodeExists)
            {
                forgotPasswordCode = random.Next(1111111111, 999999999);
            }
        }

        var minute = 5;

        user.ForgotPasswordCode = forgotPasswordCode;
        user.ForgotPasswordCodeSendDate = DateTime.Now.AddMinutes(minute);
        await userManager.UpdateAsync(user);

        var subject = "Reset Your Password";
        var body = EmailBody.CreateSendForgotPasswordCodeEmailBody(forgotPasswordCode.ToString(), minute);

        await mediator.Publish(new AuthDomainEvent(user, subject, body));

        var email = EmailBody.MaskEmail(user.Email ?? "");

        return Result<string>.Succeed($"Password recovery code is sent to your {email} email address");
    }
}

using CleanArchitecture.Application.Events;
using CleanArchitecture.Application.Utilities;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CleanArchitecture.Application.Features.Auth.ForgotPasswordEmail;

internal sealed class ForgotPasswordEmailCommandHandler(UserManager<AppUser> userManager, IMediator mediator) : IRequestHandler<ForgotPasswordEmailCommand, Result<string>>
{
    public async Task<Result<string>> Handle(ForgotPasswordEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.Users.Where(p => p.UserName == request.UserNameOrEmail || p.Email == request.UserNameOrEmail).FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            return Result<string>.Failure((int)HttpStatusCode.NotFound, "User not found!");
        }

        Random? random = new();

        var emailConfirmCode = random.Next(111111, 999999);
        var isEmailConfirmCodeExists = true;

        while (isEmailConfirmCodeExists)
        {
            isEmailConfirmCodeExists = await userManager.Users.AnyAsync(p => p.EmailConfirmCode == emailConfirmCode, cancellationToken);

            if (isEmailConfirmCodeExists)
            {
                emailConfirmCode = random.Next(111111, 999999);
            }
        }

        user.EmailConfirmed = false;
        user.EmailConfirmCode = emailConfirmCode;
        await userManager.UpdateAsync(user);


        await mediator.Publish(new AuthDomainEvent(user));

        return Result<string>.Succeed("Your user password has been updated. Verification code has been sent via email.");
    }
}

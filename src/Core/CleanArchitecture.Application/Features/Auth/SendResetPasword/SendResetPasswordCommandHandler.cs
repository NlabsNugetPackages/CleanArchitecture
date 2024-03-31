using CleanArchitecture.Application.Events;
using CleanArchitecture.Application.Utilities;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Features.Auth.SendResetPasword;

internal sealed class SendResetPasswordCommandHandler(UserManager<AppUser> userManager, IMediator mediator) : IRequestHandler<SendResetPasswordCommand, Result<string>>
{
    public async Task<Result<string>> Handle(SendResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.Users.Where(p => p.UserName == request.UserNameOrEmail || p.Email == request.UserNameOrEmail).FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            return Result<string>.Failure( "User not found!");
        }

        if (request.ReNewPassword != request.NewPassword)
        {
            return Result<string>.Failure( "Passwords do not match!");
        }

        if (user.EmailConfirmCode != request.EmailConfirmCode)
        {
            return Result<string>.Failure("Email Verification code is not correct.!");
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

        //var result = await userManager.ResetPasswordAsync(user, request.ResetToken, newPassword);

        //if (result.Succeeded)
        //{
        //    return Result<string>.Succeed("Your user password has been updated. Verification code has been sent via email.");
        //}
        //else
        //{
        //    return Result<string>.Failure( "Failed to reset password.");
        //}

        await userManager.UpdateAsync(user);


        await mediator.Publish(new AuthDomainEvent(user));

        return Result<string>.Succeed("Your password has been reset verification code sent.");
    }
}

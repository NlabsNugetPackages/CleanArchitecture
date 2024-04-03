using CleanArchitecture.Application.Utilities;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CleanArchitecture.Application.Features.Auth.SendResetPasword;

internal sealed class ChangePasswordWithForgotPasswordCodeCommandHandler(UserManager<AppUser> userManager, IMediator mediator) : IRequestHandler<ChangePasswordWithForgotPasswordCodeCommand, Result<string>>
{
    public async Task<Result<string>> Handle(ChangePasswordWithForgotPasswordCodeCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(p => p.UserName == request.UserNameOrEmail || p.Email == request.UserNameOrEmail,cancellationToken);

        if (user is null)
        {
            return Result<string>.Failure( "User not found!");
        }

        if (request.ReNewPassword != request.NewPassword)
        {
            return Result<string>.Failure( "Passwords do not match!");
        }

        if (request.ForgotPasswordCode != user.ForgotPasswordCode)
        {
            return Result<string>.Failure( "Password reset code is not correct.!");
        }

        if (user.ForgotPasswordCodeSendDate < DateTime.Now)
        {
            return Result<string>.Failure( "Your recovery password code is invalid");
        }

        var token = await userManager.GeneratePasswordResetTokenAsync(user);

        var result = await userManager.ResetPasswordAsync(user, token, request.NewPassword);

        user.ForgotPasswordCode = null;
        user.ForgotPasswordCodeSendDate = null;

        result = await userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return Result<string>.Failure(result.Errors.Select(s => s.Description).ToList());
        }

        return Result<string>.Succeed("Your password is changed. You can sign in using new password");
    }
}

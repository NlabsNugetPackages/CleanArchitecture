using CleanArchitecture.Application.Utilities;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CleanArchitecture.Application.Features.Auth.SendConfirmEmail;

internal sealed class SendConfirmEmailCommandHandler(UserManager<AppUser> userManager) : IRequestHandler<SendConfirmEmailCommand, Result<string>>
{
    public async Task<Result<string>> Handle(SendConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.Users.Where(u => u.Email == request.UserNameOrEmail || u.UserName == request.UserNameOrEmail).FirstOrDefaultAsync(cancellationToken);
        if (user is null)
        {
            return Result<string>.Failure((int)HttpStatusCode.NotFound, "User not found!");
        }

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

        user.EmailConfirmCode = emailConfirmCode;

        var result = await userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            return Result<string>.Failure("An error occurred during the update process!");
        }

        return Result<string>.Succeed("User updated successfully");
    }
}

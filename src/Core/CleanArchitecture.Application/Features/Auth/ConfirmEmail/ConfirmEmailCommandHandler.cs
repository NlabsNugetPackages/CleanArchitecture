using CleanArchitecture.Application.Utilities;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Features.Auth.ConfirmEmail;

internal sealed class ConfirmEmailCommandHandler(UserManager<AppUser> userManager) : IRequestHandler<ConfirmEmailCommand, Result<string>>
{
    public async Task<Result<string>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.Users.Where(u => u.EmailConfirmCode == request.EmailConfirmCode).FirstOrDefaultAsync(cancellationToken);
        if (user is null)
        {
            return Result<string>.Failure( "Email confirm code is not available");
        }

        if (user.EmailConfirmed)
        {
            return Result<string>.Failure( "This account has already been verified!");
        }

        if (user.EmailConfirmCodeSendDate.AddMinutes(10) < DateTime.Now)
        {
            return Result<string>.Failure( "Email verification code is invalid and has expired");
        }

        user.EmailConfirmed = true;
        await userManager.UpdateAsync(user);

        return Result<string>.Succeed("Email verification is succeed");
    }
}
using CleanArchitecture.Application.Utilities;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CleanArchitecture.Application.Features.Auth.ForgotPasswordEmail;
public sealed record ForgotPasswordEmailCommand(string UserNameOrEmail) : IRequest<Result<string>>;

internal sealed class ForgotPasswordEmailCommandHandler(UserManager<AppUser> userManager) : IRequestHandler<ForgotPasswordEmailCommand, Result<string>>
{
    public async Task<Result<string>> Handle(ForgotPasswordEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.Users.Where(p => p.UserName == request.UserNameOrEmail || p.Email == request.UserNameOrEmail).FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            return Result<string>.Failure((int)HttpStatusCode.NotFound, "User not found!");
        }


        return Result<string>.Succeed("User created successfully");
    }
}

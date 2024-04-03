using AutoMapper;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Application.Utilities;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Features.Auth.Login;

internal sealed class LoginCommandHandler(UserManager<AppUser> userManager, IJwtProvider jwtProvider, IMapper mapper) : IRequestHandler<LoginCommand, Result<LoginCommandResponse>>
{
    public async Task<Result<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.Users.Where(p => p.UserName == request.UserNameOrEmail || p.Email == request.UserNameOrEmail).FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            return Result<LoginCommandResponse>.Failure( "User not found!");
        }

        if (user.EmailConfirmed is false)
        {
            return Result<LoginCommandResponse>.Failure( "This registration has not been approved, please confirm your registration!");
        }

        var checkPassword = await userManager.CheckPasswordAsync(user, request.Password);
        if (!checkPassword)
        {
            return Result<LoginCommandResponse>.Failure( "Wrong password!");
        }

        var token = await jwtProvider.CreateTokenAsync(user, request.RememberMe);

        var response = new LoginCommandResponse(
                                AccessToken: token.AccessToken,
                                RefreshToken: user.RefreshToken!,
                                RehfreshTokenExpires: user.RefreshTokenExpires!.Value,
                                UserId: user.Id
                            );

        return Result<LoginCommandResponse>.Succeed(response);
    }
}


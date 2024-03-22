using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Features.Auth.Login;

internal sealed class LoginCommandHandler(UserManager<AppUser> userManager, IJwtProvider jwtProvider) : IRequestHandler<LoginCommand, LoginCommandResponse>
{
    public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.Users.Where(p => p.UserName == request.UserNameOrEmail || p.Email == request.UserNameOrEmail).FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            throw new ArgumentException("Kullanıcı bulunamadı!");
        }

        bool checkPassword = await userManager.CheckPasswordAsync(user, request.Password);
        if (!checkPassword)
        {
            throw new ArgumentException("Şifre yanlış!");
        }

        var token = await jwtProvider.CreateTokenAsync(user, request.RememberMe);


        return new (AccessToken: token.AccessToken, RefreshToken: user.RefreshToken, RehfreshTokenExpires: user.RefreshTokenExpires.Value, UserId: user.Id);
    }
}


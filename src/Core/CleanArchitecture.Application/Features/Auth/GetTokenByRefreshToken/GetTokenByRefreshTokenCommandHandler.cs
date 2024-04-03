using CleanArchitecture.Application.Features.Auth.Login;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Application.Utilities;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Features.Auth.GetTokenByRefreshToken;

internal sealed class GetTokenByRefreshTokenCommandHandler(UserManager<AppUser> userManager, IJwtProvider jwtProvider) : IRequestHandler<GetTokenByRefreshTokenCommand, Result<LoginCommandResponse>>
{
    public async Task<Result<LoginCommandResponse>> Handle(GetTokenByRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.Users.Where(u => u.RefreshToken == request.RefreshToken).FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            return Result<LoginCommandResponse>.Failure( "Refresh token unavailable!");
        }

        var response = await jwtProvider.CreateTokenAsync(user, false);

        return response;
    }
}

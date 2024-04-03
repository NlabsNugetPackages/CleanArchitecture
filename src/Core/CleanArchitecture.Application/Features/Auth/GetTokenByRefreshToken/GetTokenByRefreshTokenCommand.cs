using CleanArchitecture.Application.Features.Auth.Login;
using CleanArchitecture.Application.Utilities;
using MediatR;

namespace CleanArchitecture.Application.Features.Auth.GetTokenByRefreshToken;
public sealed record GetTokenByRefreshTokenCommand(
    string RefreshToken
) : IRequest<Result<LoginCommandResponse>>;

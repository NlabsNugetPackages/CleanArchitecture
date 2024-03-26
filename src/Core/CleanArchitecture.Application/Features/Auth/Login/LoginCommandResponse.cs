namespace CleanArchitecture.Application.Features.Auth.Login;
public sealed record LoginCommandResponse(
    string AccessToken,
    string RefreshToken,
    DateTime RehfreshTokenExpires,
    string UserId,
    int StatusCode
);

using MediatR;

namespace CleanArchitecture.Application.Features.Auth.Login;
public sealed record LoginCommand(
    string UserNameOrEmail,
    string Password,
    bool RememberMe
) : IRequest<LoginCommandResponse>;


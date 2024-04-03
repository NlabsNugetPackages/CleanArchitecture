using CleanArchitecture.Application.Utilities;
using MediatR;

namespace CleanArchitecture.Application.Features.Auth.SendConfirmEmail;
public sealed record SendConfirmEmailCommand(
    string UserNameOrEmail
) : IRequest<Result<string>>;

using CleanArchitecture.Application.Utilities;
using MediatR;

namespace CleanArchitecture.Application.Features.Auth.ForgotPasswordEmail;
public sealed record ForgotPasswordEmailCommand(
    string UserNameOrEmail
) : IRequest<Result<string>>;

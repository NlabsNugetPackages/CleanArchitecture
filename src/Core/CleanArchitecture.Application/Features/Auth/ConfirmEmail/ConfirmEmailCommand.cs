using CleanArchitecture.Application.Utilities;
using MediatR;

namespace CleanArchitecture.Application.Features.Auth.ConfirmEmail;
public sealed record ConfirmEmailCommand(
    int EmailConfirmCode
) : IRequest<Result<string>>;

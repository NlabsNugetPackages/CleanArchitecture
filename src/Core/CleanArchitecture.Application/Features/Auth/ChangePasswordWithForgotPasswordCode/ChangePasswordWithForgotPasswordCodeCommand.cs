using CleanArchitecture.Application.Utilities;
using MediatR;

namespace CleanArchitecture.Application.Features.Auth.SendResetPasword;
public sealed record ChangePasswordWithForgotPasswordCodeCommand(
    string UserNameOrEmail,
    int ForgotPasswordCode,
    string NewPassword,
    string ReNewPassword
) : IRequest<Result<string>>;

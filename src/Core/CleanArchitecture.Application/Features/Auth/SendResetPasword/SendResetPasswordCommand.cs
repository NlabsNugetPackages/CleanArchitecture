using CleanArchitecture.Application.Utilities;
using MediatR;

namespace CleanArchitecture.Application.Features.Auth.SendResetPasword;
public sealed record SendResetPasswordCommand(int EmailConfirmCode, string UserNameOrEmail, string NewPassword, string ReNewPassword) : IRequest<Result<string>>;

using CleanArchitecture.Application.Utilities;
using FluentValidation;
using MediatR;

namespace CleanArchitecture.Application.Features.Auth.ForgotPasswordEmail;
public sealed record ForgotPasswordEmailCommand(string UserNameOrEmail) : IRequest<Result<string>>;


public class ForgotPasswordEmailCommandValidator : AbstractValidator<ForgotPasswordEmailCommand>
{
    public ForgotPasswordEmailCommandValidator()
    {
            RuleFor(u => u.UserNameOrEmail).NotEmpty().NotNull().WithMessage("UserName Or Email cannot be empty!");
        RuleFor(u => u.UserNameOrEmail).MinimumLength(6).WithMessage("UserName Or Email must be at least 6 characters!");
    }
}
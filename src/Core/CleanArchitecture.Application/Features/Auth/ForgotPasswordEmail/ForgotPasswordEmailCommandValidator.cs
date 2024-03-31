using FluentValidation;

namespace CleanArchitecture.Application.Features.Auth.ForgotPasswordEmail;

public class ForgotPasswordEmailCommandValidator : AbstractValidator<ForgotPasswordEmailCommand>
{
    public ForgotPasswordEmailCommandValidator()
    {
            RuleFor(u => u.UserNameOrEmail).NotEmpty().NotNull().WithMessage("UserName Or Email cannot be empty!");
        RuleFor(u => u.UserNameOrEmail).MinimumLength(6).WithMessage("UserName Or Email must be at least 6 characters!");
    }
}
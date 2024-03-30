using FluentValidation;

namespace CleanArchitecture.Application.Features.Auth.ConfirmEmail;

public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
{
    public ConfirmEmailCommandValidator()
    {
        RuleFor(u => u.emailConfirmCode).NotEmpty().NotNull().WithMessage("Email verification code cannot be empty!");
        RuleFor(u => u.emailConfirmCode).GreaterThan(6).WithMessage("Email verification code must be 6 characters!");
    }
}
using FluentValidation;

namespace CleanArchitecture.Application.Features.Auth.ConfirmEmail;

public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
{
    public ConfirmEmailCommandValidator()
    {
        RuleFor(u => u.EmailConfirmCode).NotEmpty().NotNull().WithMessage("Email verification code cannot be empty!");
        RuleFor(u => u.EmailConfirmCode).GreaterThan(6).WithMessage("Email verification code must be 6 characters!");
    }
}
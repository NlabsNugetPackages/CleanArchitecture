using FluentValidation;

namespace CleanArchitecture.Application.Features.Auth.SendConfirmEmail;

public class SendConfirmEmailCommandValidator : AbstractValidator<SendConfirmEmailCommand>
{
    public SendConfirmEmailCommandValidator()
    {
        RuleFor(u => u.UserNameOrEmail).NotNull().NotEmpty().WithMessage("UserName Or Email Not null");
        RuleFor(u => u.UserNameOrEmail).MinimumLength(3).WithMessage("UserName Or Email must be at least 3 characters.");
    }
}
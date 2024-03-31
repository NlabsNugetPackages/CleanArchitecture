using FluentValidation;

namespace CleanArchitecture.Application.Features.Auth.SendResetPasword;

public class SendResetPasswordCommandValidator : AbstractValidator<SendResetPasswordCommand>
{
    public SendResetPasswordCommandValidator()
    {
        RuleFor(u => u.EmailConfirmCode).NotEmpty().NotNull().WithMessage("EmailConfirmCode cannot be empty!");
        RuleFor(u => u.EmailConfirmCode).GreaterThan(6).WithMessage("EmailConfirmCode must be at least 6 characters!");

        RuleFor(u => u.UserNameOrEmail).NotEmpty().NotNull().WithMessage("UserName Or Email cannot be empty!");
        RuleFor(u => u.UserNameOrEmail).MinimumLength(6).WithMessage("UserName Or Email must be at least 6 characters!");

        RuleFor(p => p.NewPassword).NotEmpty().WithMessage("NewPassword cannot be null");
        RuleFor(p => p.NewPassword).NotNull().WithMessage("NewPassword cannot be null");
        RuleFor(p => p.NewPassword).MinimumLength(6).WithMessage("NewPassword has to contain more than 6 character");
        RuleFor(p => p.NewPassword).Matches("[A-Z]").WithMessage("NewPassword has to contain at least 1 uppercase character");
        RuleFor(p => p.NewPassword).Matches("[a-z]").WithMessage("NewPassword has to contain at least 1 lowercase character");
        RuleFor(p => p.NewPassword).Matches("[0-9]").WithMessage("NewPassword has to contain at least 1 number");
        RuleFor(p => p.NewPassword).Matches("[^a-zA-Z0-9]").WithMessage("PasNewPasswordsword has to contain at least 1 special character");

        RuleFor(p => p.ReNewPassword).NotEmpty().WithMessage("ReNewPassword cannot be null");
        RuleFor(p => p.ReNewPassword).NotNull().WithMessage("ReNewPassword cannot be null");
        RuleFor(p => p.ReNewPassword).MinimumLength(6).WithMessage("ReNewPassword has to contain more than 6 character");
        RuleFor(p => p.ReNewPassword).Matches("[A-Z]").WithMessage("ReNewPassword has to contain at least 1 uppercase character");
        RuleFor(p => p.ReNewPassword).Matches("[a-z]").WithMessage("ReNewPassword has to contain at least 1 lowercase character");
        RuleFor(p => p.ReNewPassword).Matches("[0-9]").WithMessage("ReNewPassword has to contain at least 1 number");
        RuleFor(p => p.ReNewPassword).Matches("[^a-zA-Z0-9]").WithMessage("ReNewPassword has to contain at least 1 special character");
    }
}
using CleanArchitecture.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Application.Features.Auth.Register;
public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator(UserManager<AppUser> userManager)
    {
        RuleFor(p => p.FirstName).NotEmpty().NotNull().WithMessage("FirstName not null");
        RuleFor(p => p.FirstName).MinimumLength(3).WithMessage("FirstName must be at least 3 characters.");
        RuleFor(p => p.FirstName).NotEmpty().NotNull().WithMessage("LastName not null");
        RuleFor(p => p.FirstName).MinimumLength(3).WithMessage("LastName must be at least 3 characters.");
        RuleFor(p => p.LastName).MinimumLength(3);

        RuleFor(p => p.Email).NotEmpty().NotNull().WithMessage("Email not null");
        RuleFor(p => p.Email).EmailAddress().MinimumLength(3).WithMessage("Email must be at least 3 characters.");
        RuleFor(p => p.UserName).NotEmpty().NotNull().WithMessage("UserName not null");
        RuleFor(p => p.UserName).MinimumLength(3).WithMessage("UserName must be at least 3 characters.");
    }
}

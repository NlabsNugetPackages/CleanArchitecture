using FluentValidation;

namespace CleanArchitecture.Application.Features.Auth.GetTokenByRefreshToken;

public class GetTokenByRefreshTokenCommandValidator : AbstractValidator<GetTokenByRefreshTokenCommand>
{
    public GetTokenByRefreshTokenCommandValidator()
    {
        RuleFor(u => u.RefreshToken).NotEmpty().NotNull().WithMessage("Token cannot be empty!");
    }
}
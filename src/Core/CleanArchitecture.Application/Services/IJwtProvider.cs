using CleanArchitecture.Application.Features.Auth.Login;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Services;
public interface IJwtProvider
{
    Task<LoginCommandResponse> CreateTokenAsync(AppUser user, bool rememberMe);
}

using CleanArchitecture.Application.Features.Auth.Login;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CleanArchitecture.Infrastructure.Authentication;
internal class JwtProvider(UserManager<AppUser> userManager, JwtOptions jwtOptions) : IJwtProvider
{
    public async Task<LoginCommandResponse> CreateTokenAsync(AppUser user, bool rememberMe)
    {
        Claim[] claims = new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim(ClaimTypes.Email, user.Email ?? ""),
            new Claim("UserName", user.UserName ?? ""),
            new Claim("UserRole", user.UserRole.ToString() ?? "")
        };
        var expires = rememberMe ? DateTime.UtcNow.AddMonths(1) : DateTime.UtcNow.AddHours(1);

        var jwtSecurityToken = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: expires,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)), SecurityAlgorithms.HmacSha256));

        string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        string refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpires = expires.AddMinutes(15);
        await userManager.UpdateAsync(user);

        LoginCommandResponse response = new(
            token,
            refreshToken,
            user.RefreshTokenExpires.Value,
            user.Id);

        return response;
    }
}

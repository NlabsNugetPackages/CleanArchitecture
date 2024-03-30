using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Events;
public sealed class AuthDomainEvent : INotification
{
    public readonly AppUser _user;

    public string Subject { get; set; } = string.Empty;
    public string ToEmail { get; set; } = string.Empty;
    public string EmailConfirmCode { get; set; } = string.Empty;

    public AuthDomainEvent(AppUser user, string subject, string toEmail, string emailConfirmCode)
    {
        _user = user;
        Subject = subject;
        ToEmail = toEmail;
        EmailConfirmCode = emailConfirmCode;
    }
}
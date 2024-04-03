using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Events.Auth;
public sealed class AuthDomainEvent : INotification
{
    public readonly AppUser _user;

    public readonly string _subject;
    public readonly string _body;

    public AuthDomainEvent(AppUser user, string subject, string body)
    {
        _user = user;
        _subject = subject;
        _body = body;
    }
}
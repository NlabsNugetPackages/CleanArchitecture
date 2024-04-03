using CleanArchitecture.Application.Options;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace CleanArchitecture.Application.Events.Auth;

public sealed class SendEmailDomainEvent : INotificationHandler<AuthDomainEvent>
{
    private readonly IConfiguration _configuration;

    public SendEmailDomainEvent(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task Handle(AuthDomainEvent notification, CancellationToken cancellationToken)
    {
        var emailOptions = _configuration.GetSection("Email").Get<EmailOptions>();

        using (MailMessage mail = new MailMessage())
        {
            var subject = notification._subject;
            mail.From = new MailAddress(emailOptions!.Email);
            mail.To.Add(notification._user.Email ?? "");
            mail.Subject = subject;
            mail.Body = notification._body;
            mail.IsBodyHtml = true;

            using (var smtp = new SmtpClient(emailOptions.SMTP, emailOptions.PORT))
            {
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(emailOptions.Email, emailOptions.Password);
                smtp.EnableSsl = emailOptions.SSL;
                smtp.Port = emailOptions.PORT;
                await smtp.SendMailAsync(mail);
            }
        }
    }
}

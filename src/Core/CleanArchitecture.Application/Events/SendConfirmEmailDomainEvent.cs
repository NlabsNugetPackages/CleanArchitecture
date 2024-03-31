using CleanArchitecture.Application.Options;
using CleanArchitecture.Infrastructure.Extensions;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace CleanArchitecture.Application.Events;

public sealed class SendConfirmEmailDomainEvent : INotificationHandler<AuthDomainEvent>
{
    private readonly IConfiguration _configuration;

    public SendConfirmEmailDomainEvent(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task Handle(AuthDomainEvent notification, CancellationToken cancellationToken)
    {
        var emailOptions = _configuration.GetSection("Email").Get<EmailOptions>();
        
        using (MailMessage mail = new MailMessage())
        {
            var subject = "User Account Verification Process";
            mail.From = new MailAddress(emailOptions!.Email);
            mail.To.Add(notification._user.Email ?? "");
            mail.Subject = subject;
            mail.Body = EmailBody.CreateConfirmEmailBody(notification._user.EmailConfirmCode.ToString());
            mail.IsBodyHtml = true;

            using (SmtpClient smtp = new SmtpClient(emailOptions.SMTP, emailOptions.PORT))
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

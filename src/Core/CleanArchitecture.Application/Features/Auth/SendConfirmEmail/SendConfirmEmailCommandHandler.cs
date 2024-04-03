using CleanArchitecture.Application.Events.Auth;
using CleanArchitecture.Application.Utilities;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Features.Auth.SendConfirmEmail;

internal sealed class SendConfirmEmailCommandHandler(UserManager<AppUser> userManager, IMediator mediator) : IRequestHandler<SendConfirmEmailCommand, Result<string>>
{
    public async Task<Result<string>> Handle(SendConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.Users.Where(u => u.Email == request.UserNameOrEmail || u.UserName == request.UserNameOrEmail).FirstOrDefaultAsync(cancellationToken);
        if (user is null)
        {
            return Result<string>.Failure( "User not found!");
        }

        if (user.EmailConfirmed)
        {
            return Result<string>.Failure( "User email already confirmed");
        }

        var diff = DateTime.Now - user.EmailConfirmCodeSendDate;

        if (diff.TotalMinutes < 2)
        {
            return Result<string>.Failure( "Verification mail is send every 2 minutes.");
        }

        var minute = 5;

        user.EmailConfirmCodeSendDate = DateTime.Now.AddMinutes(minute);

        var result = await userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            return Result<string>.Failure("An error occurred during the update process!");
        }

        var subject = "Verification Mail";
        var body = EmailBody.CreateConfirmEmailBody(user.EmailConfirmCode.ToString(), minute);
        await mediator.Publish(new AuthDomainEvent(user, subject, body));

        return Result<string>.Succeed("Verification mail is sent successfully");
    }
}

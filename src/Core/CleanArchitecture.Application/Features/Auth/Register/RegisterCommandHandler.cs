using AutoMapper;
using CleanArchitecture.Application.Events.Auth;
using CleanArchitecture.Application.Utilities;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Features.Auth.Register;
public sealed class RegisterCommandHandler(UserManager<AppUser> userManager, IMapper mapper, IMediator mediator) : IRequestHandler<RegisterCommand, Result<string>>
{
    public async Task<Result<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (request.Email is null)
        {
            return Result<string>.Failure( "Email cannot be empty!");
        }

        var isEmailExists = await userManager.Users.AnyAsync(u => u.Email == request.Email);
        if (isEmailExists)
        {
            return Result<string>.Failure( "Email already has taken!");
        }

        var isUserNameExists = await userManager.Users.AnyAsync(u => u.UserName == request.UserName);
        if (isUserNameExists)
        {
            return Result<string>.Failure( "User name already has taken");
        }


        AppUser? user = mapper.Map<AppUser>(request);

        Random? random = new();
        var isEmailConfirmCodeExists = true;
        while (isEmailConfirmCodeExists)
        {
            user.EmailConfirmCode = random.Next(1111111111, 999999999);

            if (!userManager.Users.Any(u => u.EmailConfirmCode == user.EmailConfirmCode))
            {
                isEmailConfirmCodeExists = false;
            }
        }

        var minute = 5;

        user.EmailConfirmCodeSendDate = DateTime.Now.AddMinutes(minute);

        if (request.Password is null || request.RePassword is null)
        {
            return Result<string>.Failure( "Password OR re-password cannot be empty");
        }

        if (request.RePassword != request.Password)
        {
            return Result<string>.Failure( "Password and password repeat do not match.");
        }

        var result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            return Result<string>.Failure("An error occurred while registering, please try again.");
        }

        string subject = "Verification Mail" ?? "";
        var body = EmailBody.CreateSendForgotPasswordCodeEmailBody(user.EmailConfirmCode.ToString(), minute);
        await mediator.Publish(new AuthDomainEvent(user,subject, body));

        return Result<string>.Succeed("User created successfully");
    }
}

﻿using CleanArchitecture.Application.Features.Auth.ConfirmEmail;
using CleanArchitecture.Application.Features.Auth.ForgotPasswordEmail;
using CleanArchitecture.Application.Features.Auth.GetTokenByRefreshToken;
using CleanArchitecture.Application.Features.Auth.Login;
using CleanArchitecture.Application.Features.Auth.Register;
using CleanArchitecture.Application.Features.Auth.SendConfirmEmail;
using CleanArchitecture.Application.Features.Auth.SendResetPasword;
using CleanArchitecture.Presentation.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Controllers;
public sealed class AuthController : ApiController
{
    public AuthController(IMediator mediator) : base(mediator) {}

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginCommand request, CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterCommand request, CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> GetTokenByRefreshToken(GetTokenByRefreshTokenCommand request, CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> SendConfirmMail(SendConfirmEmailCommand request, CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmEmail(ConfirmEmailCommand request, CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> SendForgotPasswordEmail(ForgotPasswordEmailCommand request, CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> ChangePasswordWithForgotPasswordCode(ChangePasswordWithForgotPasswordCodeCommand request, CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}

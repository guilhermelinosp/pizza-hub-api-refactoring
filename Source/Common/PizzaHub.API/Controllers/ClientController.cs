﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaHub.Application.UseCases.Accounts.EmailConfirmation;
using PizzaHub.Application.UseCases.Accounts.ForgotPassword;
using PizzaHub.Application.UseCases.Accounts.ResetPassword;
using PizzaHub.Application.UseCases.Accounts.SignIn;
using PizzaHub.Application.UseCases.Accounts.SignUp;
using PizzaHub.Domain.Dtos.Requests.Account;
using PizzaHub.Domain.Dtos.Responses.Account;
using PizzaHub.Domain.Dtos.Responses.Exceptions;

namespace PizzaHub.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ClientController : ControllerBase
{
    private readonly IEmailConfirmation _emailConfirmation;
    private readonly IForgotPasswordUseCase _forgotPassword;
    private readonly IResetPasswordUseCase _resetPassword;

    private readonly ISignInUseCase _signIn;
    private readonly ISignUpUseCase _signUp;

    public ClientController(ISignUpUseCase signUp, ISignInUseCase signIn, IForgotPasswordUseCase forgotPassword,
        IResetPasswordUseCase resetPassword, IEmailConfirmation emailConfirmation)
    {
        _signUp = signUp;
        _signIn = signIn;
        _forgotPassword = forgotPassword;
        _resetPassword = resetPassword;
        _emailConfirmation = emailConfirmation;
    }

    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
    {
        await _signUp.SignUpAsync(request);
        return Ok(new { message = "Confirm your email to use your account." });
    }

    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [HttpPost("email-confirmation")]
    public async Task<IActionResult> EmailConfirmation([FromBody] EmailConfirmationRequest request)
    {
        await _emailConfirmation.EmailConfirmationAsync(request);
        return Ok(new { message = "Email confirmed successfully." });
    }

    [AllowAnonymous]
    [ProducesResponseType(typeof(SignInResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
    {
        var response = await _signIn.SignInAsync(request);
        return Ok(response);
    }

    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        await _forgotPassword.ForgoPasswordAsync(request);
        return Ok(new { message = "Security code sent to your email." });
    }

    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        await _resetPassword.ResetPasswordAsync(request);
        return Ok(new { message = "Password reset successful." });
    }
}
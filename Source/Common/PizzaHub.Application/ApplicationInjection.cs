using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PizzaHub.Application.Services.AutoMapper;
using PizzaHub.Application.Services.Cryptography;
using PizzaHub.Application.Services.Tokenization;
using PizzaHub.Application.UseCases.Accounts.EmailConfirmation;
using PizzaHub.Application.UseCases.Accounts.ForgotPassword;
using PizzaHub.Application.UseCases.Accounts.ResetPassword;
using PizzaHub.Application.UseCases.Accounts.SignIn;
using PizzaHub.Application.UseCases.Accounts.SignUp;

namespace PizzaHub.Application;

public static class ApplicationInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ISignUpUseCase, SignUpUseCase>();
        services.AddScoped<ISignInUseCase, SignInUseCase>();
        services.AddScoped<IForgotPasswordUseCase, ForgotPasswordUseCase>();
        services.AddScoped<IResetPasswordUseCase, ResetPasswordUseCase>();
        services.AddScoped<IEmailConfirmation, EmailConfirmation>();
        
        services.AddSingleton(new MapperConfiguration(cfg => { cfg.AddProfile(new AppAutoMapper()); }).CreateMapper());

        services.AddScoped<IEncryptService, EncryptService>();
        services.AddSingleton<ITokenService, TokenService>();
        
        
        return services;
    }
}
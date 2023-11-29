using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PizzaHub.Application.Services.Cryptography;
using PizzaHub.Application.Services.Tokenization;
using PizzaHub.Domain.Dtos.Requests.Account;
using PizzaHub.Domain.Dtos.Responses.Account;
using PizzaHub.Domain.Repositories;
using PizzaHub.Exceptions;
using PizzaHub.Exceptions.Exceptions;

namespace PizzaHub.Application.UseCases.Accounts.SignIn;

public class SignInUseCase : ISignInUseCase
{
    private readonly IConfiguration _configuration;
    private readonly IEncryptService _encrypt;
    private readonly IClientRepository _repository;
    private readonly ITokenService _token;

    public SignInUseCase(IClientRepository repository, IEncryptService encryptService, ITokenService tokenService,
        IConfiguration configuration)
    {
        _repository = repository;
        _encrypt = encryptService;
        _token = tokenService;
        _configuration = configuration;
    }

    public async Task<SignInResponse> SignInAsync(SignInRequest request)
    {
        var validator = await new SignInValidator().ValidateAsync(request);
        if (!validator.IsValid)
            throw new ValidatorException(validator.Errors.Select(er => er.ErrorMessage).ToList());

        var account = await _repository.GetByEmailAsync(request.Email!);
        if (account is null)
            throw new ClientException(new List<string> { ErrorMessages.EMAIL_USUARIO_NAO_ENCONTRADO });
        if (account.Password != _encrypt.EncryptPassword(request.Password!))
            throw new ClientException(new List<string> { ErrorMessages.EMAIL_USUARIO_NAO_ENCONTRADO });
        if (!account.EmailConfirmed)
            throw new ClientException(new List<string> { ErrorMessages.EMAIL_USUARIO_NAO_CONFIRMADO });

        return new SignInResponse
        {
            Token = _token.GenerateToken(new IdentityUser
            {
                Id = account.AccountId.ToString(),
                PhoneNumber = account.Phone,
                Email = account.Email
            }),
            RefreshToken = _token.GenerateRefreshToken(),
            ExpiryDate =
                DateTime.UtcNow.Add(TimeSpan.Parse(_configuration["Jwt-Expiry"]!, CultureInfo.InvariantCulture))
        };
    }
}
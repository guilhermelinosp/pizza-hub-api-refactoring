using PizzaHub.Application.Services.Cryptography;
using PizzaHub.Domain.Dtos.Requests.Account;
using PizzaHub.Domain.Entities;
using PizzaHub.Domain.Repositories;
using PizzaHub.Exceptions;
using PizzaHub.Exceptions.Exceptions;
using RecipeBook.Domain.SendGrid;

namespace PizzaHub.Application.UseCases.Accounts.SignUp;

public class SignUpUseCase : ISignUpUseCase
{
    private readonly IEncryptService _encrypt;
    private readonly IClientRepository _repository;
    private readonly ISendGrid _sendGrid;

    public SignUpUseCase(IClientRepository repository, IEncryptService encrypt, ISendGrid sendGrid)
    {
        _repository = repository;
        _encrypt = encrypt;
        _sendGrid = sendGrid;
    }

    public async Task SignUpAsync(SignUpRequest request)
    {
        var validator = new SignUpValidator();

        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new ValidatorException(validationResult.Errors.Select(er => er.ErrorMessage).ToList());

        var validateEmail = await _repository.GetByEmailAsync(request.Email!);
        if (validateEmail is not null)
            throw new ClientException(new List<string> { ErrorMessages.EMAIL_USUARIO_JA_REGISTRADO });

        var validatePhone = await _repository.GetByPhoneAsync(request.Phone!);
        if (validatePhone is not null)
            throw new ClientException(new List<string> { ErrorMessages.TELEFONE_USUARIO_JA_REGISTRADO });

        var emailConfirmationCode = _encrypt.GenerateCode();

        await _repository.CreateAsync(new Client
        {
            Name = request.Name!,
            Email = request.Email!,
            Code = emailConfirmationCode,
            Password = _encrypt.EncryptPassword(request.Password!),
            Phone = request.Phone!
        });

        await _sendGrid.SendConfirmationEmailAsync(request.Email!, request.Name!, emailConfirmationCode);
    }
}
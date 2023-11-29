using PizzaHub.Application.Services.Cryptography;
using PizzaHub.Domain.Dtos.Requests.Account;
using PizzaHub.Domain.Repositories;
using PizzaHub.Exceptions;
using PizzaHub.Exceptions.Exceptions;
using RecipeBook.Domain.SendGrid;

namespace PizzaHub.Application.UseCases.Accounts.ForgotPassword;

public class ForgotPasswordUseCase : IForgotPasswordUseCase
{
    private readonly IEncryptService _encrypt;
    private readonly IClientRepository _repository;
    private readonly ISendGrid _sendGrid;


    public ForgotPasswordUseCase(IClientRepository repository, IEncryptService encrypt, ISendGrid sendGrid)
    {
        _repository = repository;
        _encrypt = encrypt;
        _sendGrid = sendGrid;
    }

    public async Task ForgoPasswordAsync(ForgotPasswordRequest request)
    {
        var validator = new ForgotPasswordValidator();

        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new ValidatorException(validationResult.Errors.Select(er => er.ErrorMessage).ToList());

        var account = await _repository.GetByEmailAsync(request.Email!);

        if (account is null)
            throw new ClientException(new List<string> { ErrorMessages.EMAIL_USUARIO_NAO_ENCONTRADO });

        var code = _encrypt.GenerateCode();

        account.Code = code;

        await _repository.UpdateAsync(account);

        await _sendGrid.SendForgotPasswordEmailAsync(request.Email!, account.Name!, code);
    }
}
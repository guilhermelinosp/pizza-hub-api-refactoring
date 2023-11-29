using PizzaHub.Domain.Dtos.Requests.Account;
using PizzaHub.Domain.Repositories;
using PizzaHub.Exceptions;
using PizzaHub.Exceptions.Exceptions;

namespace PizzaHub.Application.UseCases.Accounts.EmailConfirmation;

public class EmailConfirmation : IEmailConfirmation
{
    private readonly IClientRepository _repository;

    public EmailConfirmation(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task EmailConfirmationAsync(EmailConfirmationRequest request)
    {
        var validator = new EmailConfirmationValidator();

        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new ValidatorException(validationResult.Errors.Select(er => er.ErrorMessage).ToList());

        var account = await _repository.GetByCodeAsync(request.Code!);
        if (account is not null)
        {
            if (account.EmailConfirmed)
                throw new ClientException(new List<string>
                    { "The email has already been confirmed previously" });

            if (account.Code != request.Code)
                throw new ClientException(new List<string> { ErrorMessages.EMAIL_USUARIO_CODIGO_INVALIDO });

            account.EmailConfirmed = true;
            account.Code = string.Empty;

            await _repository.UpdateAsync(account);
        }
        else
        {
            throw new ClientException(new List<string> { ErrorMessages.EMAIL_USUARIO_NAO_ENCONTRADO });
        }
    }
}
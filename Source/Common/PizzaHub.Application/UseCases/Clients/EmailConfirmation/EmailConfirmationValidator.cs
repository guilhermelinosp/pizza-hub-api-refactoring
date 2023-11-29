using FluentValidation;
using PizzaHub.Domain.Dtos.Requests.Account;
using PizzaHub.Exceptions;

namespace PizzaHub.Application.UseCases.Accounts.EmailConfirmation;

public class EmailConfirmationValidator : AbstractValidator<EmailConfirmationRequest>
{
    public EmailConfirmationValidator()
    {
        RuleFor(c => c.Code)
            .NotEmpty()
            .WithMessage(ErrorMessages.EMAIL_USUARIO_CODIGO_INVALIDO)
            .MinimumLength(6)
            .WithMessage(ErrorMessages.EMAIL_USUARIO_CODIGO_INVALIDO)
            .MaximumLength(6)
            .WithMessage(ErrorMessages.EMAIL_USUARIO_CODIGO_INVALIDO);
    }
}
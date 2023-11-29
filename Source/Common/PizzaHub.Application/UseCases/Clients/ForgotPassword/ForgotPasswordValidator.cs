using FluentValidation;
using PizzaHub.Domain.Dtos.Requests.Account;
using PizzaHub.Exceptions;

namespace PizzaHub.Application.UseCases.Accounts.ForgotPassword;

public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordRequest>
{
    public ForgotPasswordValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty()
            .WithMessage(ErrorMessages.EMAIL_USUARIO_NAO_INFORMADO)
            .EmailAddress()
            .WithMessage(ErrorMessages.EMAIL_USUARIO_INVALIDO);
    }
}
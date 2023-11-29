using PizzaHub.Domain.Dtos.Requests.Account;

namespace PizzaHub.Application.UseCases.Accounts.EmailConfirmation;

public interface IEmailConfirmation
{
    Task EmailConfirmationAsync(EmailConfirmationRequest request);
}
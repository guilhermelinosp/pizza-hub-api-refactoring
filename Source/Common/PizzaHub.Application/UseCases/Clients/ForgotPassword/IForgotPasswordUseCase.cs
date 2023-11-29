using PizzaHub.Domain.Dtos.Requests.Account;

namespace PizzaHub.Application.UseCases.Accounts.ForgotPassword;

public interface IForgotPasswordUseCase
{
    Task ForgoPasswordAsync(ForgotPasswordRequest request);
}
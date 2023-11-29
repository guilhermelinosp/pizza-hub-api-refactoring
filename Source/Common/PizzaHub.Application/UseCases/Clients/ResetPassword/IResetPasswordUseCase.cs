using PizzaHub.Domain.Dtos.Requests.Account;

namespace PizzaHub.Application.UseCases.Accounts.ResetPassword;

public interface IResetPasswordUseCase
{
    Task ResetPasswordAsync(ResetPasswordRequest request);
}
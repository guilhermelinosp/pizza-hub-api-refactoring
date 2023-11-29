using PizzaHub.Domain.Dtos.Requests.Account;

namespace PizzaHub.Application.UseCases.Accounts.SignUp;

public interface ISignUpUseCase
{
    Task SignUpAsync(SignUpRequest request);
}
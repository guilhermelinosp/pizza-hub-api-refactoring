using PizzaHub.Domain.Dtos.Requests.Account;
using PizzaHub.Domain.Dtos.Responses.Account;

namespace PizzaHub.Application.UseCases.Accounts.SignIn;

public interface ISignInUseCase
{
    Task<SignInResponse> SignInAsync(SignInRequest request);
}
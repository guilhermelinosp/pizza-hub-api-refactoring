using AutoMapper;
using PizzaHub.Domain.Dtos.Requests.Account;
using PizzaHub.Domain.Dtos.Responses.Account;
using PizzaHub.Domain.Entities;

namespace PizzaHub.Application.Services.AutoMapper;

internal class AppAutoMapper : Profile
{
    public AppAutoMapper()
    {
        CreateMap<SignUpRequest, Client>();
        CreateMap<SignInRequest, Client>();
        CreateMap<ResetPasswordRequest, Client>();
        CreateMap<ForgotPasswordRequest, Client>();
        CreateMap<EmailConfirmationRequest, Client>();
        CreateMap<Client, AccountResponse>();
    }
}
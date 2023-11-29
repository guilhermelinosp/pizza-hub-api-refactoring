using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PizzaHub.Domain.Dtos.Responses.Exceptions;
using PizzaHub.Exceptions;
using PizzaHub.Exceptions.Exceptions;

namespace PizzaHub.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is BaseException)
            HandleException(context);
        else
            HandleUnknownException(context);
    }

    private static void HandleException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case ValidatorException:
                HandleValidationException(context);
                break;
            case ClientException:
                HandleAccountException(context);
                break;
            case TokenException:
                HandleTokenException(context);
                break;
            default:
                HandleUnknownException(context);
                break;
        }
    }

    private static void HandleValidationException(ExceptionContext context)
    {
        var exception = context.Exception as ValidatorException;
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(new ExceptionResponse(exception!.ErrorMessages!));
    }

    private static void HandleAccountException(ExceptionContext context)
    {
        var exception = context.Exception as ClientException;
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(new ExceptionResponse(exception!.ErrorMessages!));
    }

    private static void HandleUnknownException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        Console.WriteLine(context.Exception);
        context.Result = new ObjectResult(new ExceptionResponse(new List<string> { ErrorMessages.ERRO_DESCONHECIDO }));
    }


    private static void HandleTokenException(ExceptionContext context)
    {
        var exception = context.Exception as TokenException;
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(new ExceptionResponse(exception!.ErrorMessages!));
    }
}
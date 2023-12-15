
using JWTService.BLL.Models.Exceptions;
using Microsoft.AspNetCore.Http;

namespace JWTService.BLL.ExceptionHandlers;

public interface IExceptionHandler<TException> where TException: BaseException
{
    Task HandleException(HttpContext context, TException exception);
}
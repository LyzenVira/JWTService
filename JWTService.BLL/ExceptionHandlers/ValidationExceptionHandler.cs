
using JWTService.BLL.Models;
using JWTService.BLL.Models.Exceptions;
using Microsoft.AspNetCore.Http;

namespace JWTService.BLL.ExceptionHandlers;

public class ValidationExceptionHandler : IExceptionHandler<ValidationException>
{
    public async Task HandleException(HttpContext context, ValidationException exception)
    {
        context.Response.StatusCode = exception.StatusCode;
        await context.Response.WriteAsJsonAsync(ResponseEntity.CreateWithOneMessage(exception));
    }
}
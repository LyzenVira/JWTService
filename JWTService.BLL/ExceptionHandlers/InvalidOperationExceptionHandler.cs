
using JWTService.BLL.Models;
using Microsoft.AspNetCore.Http;
using InvalidOperationException = JWTService.BLL.Models.Exceptions.InvalidOperationException;

namespace JWTService.BLL.ExceptionHandlers;

public class InvalidOperationExceptionHandler : IExceptionHandler<InvalidOperationException>
{
    public async Task HandleException(HttpContext context, InvalidOperationException exception)
    {
        context.Response.StatusCode = exception.StatusCode;
        await context.Response.WriteAsJsonAsync(ResponseEntity.CreateWithOneMessage(exception));
    }
}
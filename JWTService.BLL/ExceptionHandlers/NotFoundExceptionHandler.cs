using JWTService.BLL.Models;
using JWTService.BLL.Models.Exceptions;
using Microsoft.AspNetCore.Http;

namespace JWTService.BLL.ExceptionHandlers;

public class NotFoundExceptionHandler : IExceptionHandler<NotFoundException>
{
    public async Task HandleException(HttpContext context, NotFoundException exception)
    {
        context.Response.StatusCode = exception.StatusCode;
        await context.Response.WriteAsJsonAsync(ResponseEntity.CreateWithOneMessage(exception));
    }
}
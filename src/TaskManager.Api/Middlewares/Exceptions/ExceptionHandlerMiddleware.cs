using System.Net;
using TaskManager.Domain.Exceptions;

namespace TaskManager.Api.Middlewares.Exceptions;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    private readonly IDictionary<Type, HttpStatusCode> _exceptionHttpStatuses =
        new Dictionary<Type, HttpStatusCode>
        {
            { typeof(BadRequestException), HttpStatusCode.BadRequest },
            { typeof(DomainException), HttpStatusCode.BadRequest },
            { typeof(NotFoundException), HttpStatusCode.NotFound }
        };

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            var errorMessage = "Неопознанная ошибка";

            if (ex is DomainBaseException domainException)
            {
                errorMessage = domainException switch
                {
                    NotFoundException => domainException.PublicMessage ?? "Не найдено",
                    BadRequestException or DomainException => domainException.PublicMessage ?? "Некорректный запрос",
                    _ => errorMessage
                };
            }

            if (!_exceptionHttpStatuses.TryGetValue(ex.GetType(), out var statusCode))
            {
                statusCode = HttpStatusCode.InternalServerError;
            }

            httpContext.Response.Clear();
            httpContext.Response.StatusCode = (int)statusCode;
            httpContext.Response.ContentType = "text/plain";

            await httpContext.Response.WriteAsync(errorMessage).ConfigureAwait(false);
        }
    }
}
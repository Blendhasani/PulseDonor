using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

public class ExceptionFilterMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionFilterMiddleware> _logger;

    public ExceptionFilterMiddleware(RequestDelegate next, ILogger<ExceptionFilterMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = new
        {
            error = exception.Message,
            details = exception.InnerException?.Message,
            stackTrace = exception.StackTrace
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        return context.Response.WriteAsJsonAsync(response);
    }
}

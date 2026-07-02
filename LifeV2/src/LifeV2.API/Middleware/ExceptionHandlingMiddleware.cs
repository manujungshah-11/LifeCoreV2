using System.Net;
using System.Text.Json;
using LifeV2.Application.Common;

namespace LifeV2.API.Middleware;

/// <summary>
/// Converts unhandled exceptions into RFC7807-style ProblemDetails JSON responses.
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
            _logger.LogError(ex, "Unhandled exception for {Path}", context.Request.Path);

            var (status, title) = ex switch
            {
                NotFoundException => (HttpStatusCode.NotFound, ex.Message),
                ArgumentException => (HttpStatusCode.BadRequest, ex.Message),
                _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred.")
            };

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = (int)status;

            var problem = new
            {
                type = $"https://httpstatuses.io/{(int)status}",
                title,
                status = (int)status,
                traceId = context.TraceIdentifier
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(problem));
        }
    }
}

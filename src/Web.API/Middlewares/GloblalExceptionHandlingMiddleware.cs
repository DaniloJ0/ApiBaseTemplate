using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace Web.API.Middlewares;

public class GloblalExceptionHandlingMiddleware(ILogger<GloblalExceptionHandlingMiddleware> logger) : IMiddleware
{
    private readonly ILogger<GloblalExceptionHandlingMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError("{Message}", ex.Message);
            Console.WriteLine(ex.Message);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            ProblemDetails problem = new()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = "Server Error",
                Title = "Server Error",
                Detail = "An internal server has ocurred."
            };

            string json = JsonSerializer.Serialize(problem);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(json);
        }
    }
}

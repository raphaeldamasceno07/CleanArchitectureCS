using Domain.Exceptions;

namespace API.Middleware;

public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;

    public GlobalExceptionHandler(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Domain.Exceptions.Domain ex) 
        {
            context.Response.StatusCode = 400; 
            await context.Response.WriteAsJsonAsync(new { error = ex.Message });
        }
        catch (Exception)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new { error = "Internal server error." });
        }
    }
}
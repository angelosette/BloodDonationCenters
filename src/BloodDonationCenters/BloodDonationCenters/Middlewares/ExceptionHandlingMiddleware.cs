namespace BloodDonationCenters.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (KeyNotFoundException ex)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(ex.Message);
            }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("An unexpected error occurred.");
        }
    }
}


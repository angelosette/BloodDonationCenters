using BloodDonationCenters.Application.Common;
using Microsoft.AspNetCore.Http;

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
            var result = ResultData<Exception>.Error(ex.Message);
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsJsonAsync(result);
        }
        catch (Exception ex)
        {            
            var result = ResultData<Exception>.Error("An unexpected error occurred");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(result);
        }
    }
}


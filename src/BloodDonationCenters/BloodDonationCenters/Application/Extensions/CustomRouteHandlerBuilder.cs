using BloodDonationCenters.Application.Validators;

namespace BloodDonationCenters.Application.Extensions;

public static class CustomRouteHandlerBuilder
{
    public static RouteHandlerBuilder Validate<T>(this RouteHandlerBuilder builder, bool firstErrorOnly = true)
    {
        builder.AddEndpointFilter(async (invocationContext, next) =>
        {
            var argument = invocationContext.Arguments.OfType<T>().FirstOrDefault();
            var response = argument.DataAnnotationsValidate();

            if (!response.IsValid)
            {
                string errorMessage = firstErrorOnly ?
                                        response.Results.FirstOrDefault().ErrorMessage :
                                        string.Join("|", response.Results.Select(x => x.ErrorMessage));
                return Results.Problem(errorMessage, statusCode: 400);
            }

            return await next(invocationContext);
        });

        return builder;
    }
}
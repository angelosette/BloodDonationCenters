using BloodDonationCenters.Application.Common;
using BloodDonationCenters.Application.DTOs;
using BloodDonationCenters.Application.Extensions;
using BloodDonationCenters.Application.Interfaces;
using BloodDonationCenters.Application.Mappings;

namespace BloodDonationCenters.Api.Endpoints;

public static class BloodDonationCentersEndpoints
{
    public static WebApplication AddBloodDonationCentersEndpoints(this WebApplication app)
    {
        app.MapGet("/BloodDonationCenters", async (IBloodDonationService bloodDonationService, string? country, string? city) =>
        {
            var result = await bloodDonationService.GetAllCenters(country, city);
            return result.ToOkResult();
        });

        app.MapGet("/BloodDonationCenters/{id}", async (IBloodDonationService bloodDonationService, Guid id) =>
        {
            var result = await bloodDonationService.GetCenterById(id);
            return result.ToSingleResult();
        });

        app.MapDelete("/BloodDonationCenters/{id}", async (IBloodDonationService bloodDonationService, Guid id) =>
        {
            var result = await bloodDonationService.GetCenterById(id);
            if (result is null) return Results.NotFound();
            await bloodDonationService.DeleteCenter(id);
            return Results.NoContent();
        });


        app.MapPost("/BloodDonationCenters", async (IBloodDonationService bloodDonationService, BloodDonationCenterDTO centerDTO) =>
        {
            if (centerDTO is null) return Results.BadRequest();
            var center = BloodDonationCenterMapper.ToEntity(centerDTO);
            await bloodDonationService.AddCenter(center);
            return Results.Created($"/BloodDonationCenters/{center.Id}", center);
        })
        .Validate<BloodDonationCenterDTO>();

        app.MapPut("/BloodDonationCenters/{id}", async (IBloodDonationService bloodDonationService, Guid id, BloodDonationCenterDTO centerDTO) =>
        {
            if (centerDTO is null) return Results.BadRequest();
            var center = BloodDonationCenterMapper.ToEntity(centerDTO, id);
            await bloodDonationService.UpdateCenter(center);
            return Results.NoContent();
        })
        .Validate<BloodDonationCenterDTO>();

        return app;
    }
}


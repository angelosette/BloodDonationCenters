using BloodDonationCenters.Application.DTOs;
using BloodDonationCenters.Domain.Entities;

namespace BloodDonationCenters.Application.Mappings;

public static class BloodDonationCenterMapper
{
    public static BloodDonationCenter ToEntity(BloodDonationCenterDTO dto, Guid? id = null)
    {
        return new BloodDonationCenter
        {
            Id = id ?? Guid.NewGuid(),
            Name = dto.Name,
            Country = dto.Country,
            City = dto.City,
            Address = dto.Address,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
            Phone = dto.Phone,
            Mobile = dto.Mobile,
            Email = dto.Email,
            Websites = dto.Websites,
            LastUpdate = DateTime.UtcNow,
            Information = dto.Information,
            Inventory = dto.Inventory,
            OperatingHours = dto.OperatingHours.Select(x => new OperatingHours
            {
                Day = x.Day,
                Open = x.Open,
                Close = x.Close
            }).ToList()
        };
    }
}
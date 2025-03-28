using BloodDonationCenters.Domain.Enums;

namespace BloodDonationCenters.Application.DTOs;

public record BloodDonationCenterDTO
{
    public string Name { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Website { get; set; }
    public Dictionary<BloodType, InventoryStatus> Inventory { get; set; } = [];
}


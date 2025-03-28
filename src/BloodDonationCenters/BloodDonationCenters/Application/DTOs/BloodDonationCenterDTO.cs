using BloodDonationCenters.Application.Validators;
using BloodDonationCenters.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BloodDonationCenters.Application.DTOs;

public record BloodDonationCenterDTO
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Country { get; set; }

    [Required]
    public string City { get; set; }

    [Required]
    public string Address { get; set; }

    [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90 degrees.")]
    public double Latitude { get; set; }

    [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180 degrees.")]
    public double Longitude { get; set; }

    [Phone]   
    public string? Phone { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    [Url]
    public string? Website { get; set; }
    
    [ValidateInventoryStatus]
    public Dictionary<BloodType, InventoryStatus> Inventory { get; set; } = [];
}
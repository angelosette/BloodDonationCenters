using BloodDonationCenters.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BloodDonationCenters.Domain.Entities;

public record BloodDonationCenter
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? Phone { get; set; }
    public string? Mobile { get; set; }
    public string? Email { get; set; }    
    public DateTime LastUpdate { get; set; }
    public string? Information { get; set; }    

    [JsonIgnore]
    public string BloodInventoryJson { get; set; }

    [NotMapped]
    public Dictionary<BloodType, InventoryStatus> Inventory { 
        get => JsonSerializer.Deserialize<Dictionary<BloodType, InventoryStatus>>(BloodInventoryJson ?? "{}"); 
        set => BloodInventoryJson = JsonSerializer.Serialize(value); 
    }

    [JsonIgnore]
    public string WebsitesJson { get; set; }

    [NotMapped]
    public Dictionary<WebSiteType, string> Websites
    {
        get => JsonSerializer.Deserialize<Dictionary<WebSiteType, string>>(WebsitesJson ?? "{}");
        set => WebsitesJson = JsonSerializer.Serialize(value);
    }

    [JsonIgnore]
    public string OperatingHoursJson { get; set; }

    [NotMapped]
    public List<OperatingHours> OperatingHours
        {
        get => JsonSerializer.Deserialize<List<OperatingHours>>(OperatingHoursJson ?? "[]");
        set => OperatingHoursJson = JsonSerializer.Serialize(value);
        }
    }
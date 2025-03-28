using BloodDonationCenters.Domain.Entities;
using BloodDonationCenters.Domain.Enums;

namespace BloodDonationCenters.Infraestructure.Data;

public class DbInitializer
{
    public static void Seed(AppDbContext context)
    {
        if (context.BloodDonationCenters.Any()) return;

        var centers = new List<BloodDonationCenter>
        {
            new BloodDonationCenter
            {
                Id = Guid.NewGuid(),
                Name = "Central Blood Bank",
                Country = "USA",
                City = "New York",
                Address = "123 Main St",
                Latitude = 40.7128,
                Longitude = -74.0060,
                Phone = "123-456-7890",
                Email = "contact@centralbloodbank.com",
                Website = "http://centralbloodbank.com",                    
                Inventory = new Dictionary<BloodType, InventoryStatus>
                {
                    { BloodType.APositive, InventoryStatus.Sufficient },
                    { BloodType.ANegative, InventoryStatus.Moderate },
                    { BloodType.BPositive, InventoryStatus.Low },
                    { BloodType.BNegative, InventoryStatus.CriticallyLow },
                    { BloodType.ABPositive, InventoryStatus.Surplus },
                    { BloodType.ABNegative, InventoryStatus.Low },
                    { BloodType.OPositive, InventoryStatus.Sufficient },
                    { BloodType.ONegative, InventoryStatus.Moderate }
                },
                LastUpdate = DateTime.UtcNow                
            },
            new BloodDonationCenter
            {
                Id = Guid.NewGuid(),
                Name = "Westside Blood Center",
                Country = "USA",
                City = "Los Angeles",
                Address = "456 Elm St",
                Latitude = 34.0522,
                Longitude = -118.2437,                    
                Phone = "987-654-3210",
                Email = "info@westsidebloodcenter.com",
                Website = "http://westsidebloodcenter.com",                    
                Inventory = new Dictionary<BloodType, InventoryStatus>
                {
                    { BloodType.APositive, InventoryStatus.Low },
                    { BloodType.ANegative, InventoryStatus.CriticallyLow },
                    { BloodType.BPositive, InventoryStatus.Sufficient },
                    { BloodType.BNegative, InventoryStatus.Moderate },
                    { BloodType.ABPositive, InventoryStatus.Low },
                    { BloodType.ABNegative, InventoryStatus.Surplus },
                    { BloodType.OPositive, InventoryStatus.Moderate },
                    { BloodType.ONegative, InventoryStatus.Sufficient }
                },
                LastUpdate = DateTime.UtcNow                    
            }
        };

        context.BloodDonationCenters.AddRange(centers);
        context.SaveChanges();

    }
}
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
                Id = Guid.Parse("8cd5b775-225e-4f8c-851c-33d6e2ee3bd9"),
                Name = "Central Blood Bank",
                Country = "USA",
                City = "New York",
                Address = "123 Main St",
                Latitude = 40.7128,
                Longitude = -74.0060,
                Phone = "123-456-7890",
                Mobile = "123-456-7890",
                Email = "contact@centralbloodbank.com",
                Information = "Central Blood Bank is a non-profit organization that provides blood and blood products to hospitals in the New York area.",
                Websites = new Dictionary<WebSiteType, string>
                {
                    { WebSiteType.Site, "http://centralbloodbank.com" }                    
                },
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
                OperatingHours = new List<OperatingHours>
                {
                    new OperatingHours
                    {
                        Day = DayOfWeek.Monday,
                        Open = new TimeSpan(8, 0, 0),
                        Close = new TimeSpan(17, 0, 0)
                    },
                    new OperatingHours
                    {
                        Day = DayOfWeek.Tuesday,
                        Open = new TimeSpan(8, 0, 0),
                        Close = new TimeSpan(17, 0, 0)
                    },
                    new OperatingHours
                    {
                        Day = DayOfWeek.Wednesday,
                        Open = new TimeSpan(8, 0, 0),
                        Close = new TimeSpan(17, 0, 0)
                    },
                    new OperatingHours
                    {
                        Day = DayOfWeek.Thursday,
                        Open = new TimeSpan(8, 0, 0),
                        Close = new TimeSpan(17, 0, 0)
                    },
                    new OperatingHours
                    {
                        Day = DayOfWeek.Friday,
                        Open = new TimeSpan(8, 0, 0),
                        Close = new TimeSpan(17, 0, 0)
                    }
                },
                LastUpdate = DateTime.UtcNow                
            },
            new BloodDonationCenter
            {
                Id = Guid.Parse("cc49b6b4-3aac-4dc9-8e08-1282c4142f2d"),
                Name = "Westside Blood Center",
                Country = "USA",
                City = "Los Angeles",
                Address = "456 Elm St",
                Latitude = 34.0522,
                Longitude = -118.2437,                    
                Phone = "987-654-3210",
                Mobile = "987-654-3210",
                Information = "Westside Blood Center is a non-profit organization that provides blood and blood products to hospitals in the Los Angeles area.",
                Email = "info@westsidebloodcenter.com",                
                Websites = new Dictionary<WebSiteType, string>
                {
                    { WebSiteType.Site, "http://westsidebloodcenter.com" }
                },
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
                OperatingHours = new List<OperatingHours>
                {
                    new OperatingHours
                    {
                        Day = DayOfWeek.Monday,
                        Open = new TimeSpan(9, 0, 0),
                        Close = new TimeSpan(18, 0, 0)
                    },
                    new OperatingHours
                    {
                        Day = DayOfWeek.Tuesday,
                        Open = new TimeSpan(9, 0, 0),
                        Close = new TimeSpan(18, 0, 0)
                    },
                    new OperatingHours
                    {
                        Day = DayOfWeek.Wednesday,
                        Open = new TimeSpan(9, 0, 0),
                        Close = new TimeSpan(18, 0, 0)
                    },
                    new OperatingHours
                    {
                        Day = DayOfWeek.Thursday,
                        Open = new TimeSpan(9, 0, 0),
                        Close = new TimeSpan(18, 0, 0)
                    },
                    new OperatingHours
                    {
                        Day = DayOfWeek.Friday,
                        Open = new TimeSpan(9, 0, 0),
                        Close = new TimeSpan(18, 0, 0)
                    }
                },
                LastUpdate = DateTime.UtcNow                    
            }
        };

        context.BloodDonationCenters.AddRange(centers);
        context.SaveChanges();

    }
}
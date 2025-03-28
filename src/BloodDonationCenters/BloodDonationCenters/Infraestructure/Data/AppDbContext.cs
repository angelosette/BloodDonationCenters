using BloodDonationCenters.Domain.Entities;
using BloodDonationCenters.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace BloodDonationCenters.Infraestructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }   
    public DbSet<BloodDonationCenter> BloodDonationCenters { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var inventoryConverter = new ValueConverter<Dictionary<BloodType, InventoryStatus>, string>(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
            v => JsonSerializer.Deserialize<Dictionary<BloodType, InventoryStatus>>(v, (JsonSerializerOptions)null));

        modelBuilder.Entity<BloodDonationCenter>()
            .Property(x => x.Inventory)
            .HasConversion(inventoryConverter);
    }
}
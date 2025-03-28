using BloodDonationCenters.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BloodDonationCenters.Application.Validators;

public class ValidateInventoryStatus : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var inventory = value as Dictionary<BloodType, InventoryStatus>;
        
        if (inventory.Any(x => !Enum.IsDefined(typeof(InventoryStatus), x.Value)))
        {
            return new ValidationResult("Invalid inventory status.");
        }

        return ValidationResult.Success;
    }
}
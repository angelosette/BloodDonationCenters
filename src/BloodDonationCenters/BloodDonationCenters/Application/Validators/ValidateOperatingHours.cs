using BloodDonationCenters.Application.DTOs;
using System.ComponentModel.DataAnnotations;

namespace BloodDonationCenters.Application.Validators;

public class ValidateOperatingHours : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var operatingHours = value as List<OperatingHoursDTO>;                

        foreach (var hours in operatingHours)
        {
            if (!Enum.IsDefined(typeof(DayOfWeek), hours.Day))
            {
                return new ValidationResult($"Invalid day of the week: {hours.Day}");
            }
            
            if (hours.Open >= hours.Close)
            {
                return new ValidationResult("Closing time must be after opening time.");
            }
        }

        return ValidationResult.Success;
    }
}
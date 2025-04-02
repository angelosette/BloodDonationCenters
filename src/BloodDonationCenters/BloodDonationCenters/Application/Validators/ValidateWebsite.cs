using BloodDonationCenters.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BloodDonationCenters.Application.Validators;

public class ValidateWebsite : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var websites = value as Dictionary<WebSiteType, string>;              

        foreach (var website in websites.Values)
        {
            if (!Uri.TryCreate(website, UriKind.Absolute, out var uriResult) ||
                !(uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
            {
                return new ValidationResult($"Invalid website URL: {website}");
            }
        }

        return ValidationResult.Success;
    }
}
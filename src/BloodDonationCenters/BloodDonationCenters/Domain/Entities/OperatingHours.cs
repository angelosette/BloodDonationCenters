namespace BloodDonationCenters.Domain.Entities;

public class OperatingHours
{
    public DayOfWeek Day { get; set; }
    public TimeSpan Open { get; set; }
    public TimeSpan Close { get; set; }
}

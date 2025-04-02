namespace BloodDonationCenters.Application.DTOs;

public record OperatingHoursDTO(DayOfWeek Day, TimeSpan Open, TimeSpan Close) {}
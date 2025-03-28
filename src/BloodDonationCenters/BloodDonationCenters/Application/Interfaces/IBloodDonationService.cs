using BloodDonationCenters.Domain.Entities;

namespace BloodDonationCenters.Application.Interfaces;

public interface IBloodDonationService
{
    Task<IEnumerable<BloodDonationCenter>> GetAllCenters(string? city, string? country);
    Task<BloodDonationCenter> GetCenterById(Guid id);
    Task AddCenter(BloodDonationCenter center);
    Task UpdateCenter(BloodDonationCenter center);
    Task DeleteCenter(Guid id);    
}
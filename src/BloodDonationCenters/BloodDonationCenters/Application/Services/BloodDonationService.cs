using BloodDonationCenters.Application.Interfaces;
using BloodDonationCenters.Domain.Entities;
using BloodDonationCenters.Infraestructure.Interfaces;
using LinqKit;

namespace BloodDonationCenters.Application.Services;

public class BloodDonationService : IBloodDonationService
{
    private readonly IRepository<BloodDonationCenter> _repository;

    public BloodDonationService(IRepository<BloodDonationCenter> repository)
    {
        _repository = repository;
    }

    public async Task AddCenter(BloodDonationCenter center) => await _repository.Add(center);

    public async Task DeleteCenter(Guid id)
    {
        var center = await GetCenterById(id);
        if (center == null)
            throw new KeyNotFoundException($"Blood center with ID {id} was not found.");

        await _repository.Delete(center);
    }

    public async Task<IEnumerable<BloodDonationCenter>> GetAllCenters(string? country, string? city)
    {
        var predicate = PredicateBuilder.New<BloodDonationCenter>(x => true);

        if (!string.IsNullOrEmpty(city))
            predicate = predicate.And(x => x.City.Equals(city, StringComparison.CurrentCultureIgnoreCase));

        if (!string.IsNullOrEmpty(country))
            predicate = predicate.And(x => x.Country.Equals(country, StringComparison.CurrentCultureIgnoreCase));

        return await _repository.GetAll(predicate);
    }


    public async Task<BloodDonationCenter> GetCenterById(Guid id) => await _repository.GetById(id);

    public async Task UpdateCenter(BloodDonationCenter center)
    {
        var exitingCenter = await GetCenterById(center.Id);
        if (exitingCenter == null)
            throw new KeyNotFoundException($"Blood center with ID {center.Id} was not found.");

        exitingCenter.Name = center.Name;
        exitingCenter.Country = center.Country;
        exitingCenter.City = center.City;
        exitingCenter.Address = center.Address;
        exitingCenter.Latitude = center.Latitude;
        exitingCenter.Longitude = center.Longitude;
        exitingCenter.Phone = center.Phone;
        exitingCenter.Mobile = center.Mobile;
        exitingCenter.Email = center.Email;
        exitingCenter.Websites = center.Websites;
        exitingCenter.LastUpdate = DateTime.UtcNow;
        exitingCenter.Inventory = center.Inventory;
        exitingCenter.Information = center.Information;
        exitingCenter.OperatingHours = center.OperatingHours;

        await _repository.Update(exitingCenter);
    }
}


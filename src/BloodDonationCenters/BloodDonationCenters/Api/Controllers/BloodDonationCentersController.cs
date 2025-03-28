using BloodDonationCenters.Application.DTOs;
using BloodDonationCenters.Application.Interfaces;
using BloodDonationCenters.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationCenters.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BloodDonationCentersController : ControllerBase
{
    private readonly IBloodDonationService _bloodDonationService;

    public BloodDonationCentersController(IBloodDonationService bloodDonationService)
    {
        _bloodDonationService = bloodDonationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetBloodDonationCenters([FromQuery] string? country = null, [FromQuery] string? city = null)
    {
        var bloodDonationCenters = await _bloodDonationService.GetAllCenters(country, city);
        return Ok(bloodDonationCenters);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBloodDonationCentersById(Guid id)
    {
        var center = await _bloodDonationService.GetCenterById(id);
        if (center == null) return NotFound();
        return Ok(center);
    }

    [HttpPost]
    public async Task<IActionResult> AddBloodDonationCenter([FromBody] BloodDonationCenterDTO centerDTO)
    {
        if (centerDTO == null) return BadRequest();        
        var center = MapToBloodDonationCenter(centerDTO);        
        await _bloodDonationService.AddCenter(center);
        return CreatedAtAction(nameof(GetBloodDonationCentersById), new { id = center.Id }, center);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBloodDonationCenter(Guid id, [FromBody] BloodDonationCenterDTO centerDTO)
    {
        if (centerDTO == null) return BadRequest();        
        var center = MapToBloodDonationCenter(centerDTO, id);        
        await _bloodDonationService.UpdateCenter(center);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBloodDonationCenter(Guid id)
    {
        var center = await _bloodDonationService.GetCenterById(id);
        if (center == null) return NotFound();
        await _bloodDonationService.DeleteCenter(id);
        return NoContent();
    }


    private BloodDonationCenter MapToBloodDonationCenter(BloodDonationCenterDTO dto, Guid? id = null)
    {
        return new BloodDonationCenter
        {
            Id = id ?? Guid.NewGuid(),
            Name = dto.Name,
            Country = dto.Country,
            City = dto.City,
            Address = dto.Address,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
            Phone = dto.Phone,
            Email = dto.Email,
            Website = dto.Website,
            LastUpdate = DateTime.UtcNow,
            Inventory = dto.Inventory
        };
    }
}


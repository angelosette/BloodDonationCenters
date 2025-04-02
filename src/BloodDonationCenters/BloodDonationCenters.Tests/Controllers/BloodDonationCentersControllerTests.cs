using BloodDonationCenters.Api.Controllers;
using BloodDonationCenters.Application.DTOs;
using BloodDonationCenters.Application.Interfaces;
using BloodDonationCenters.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BloodDonationCenters.Tests.Controllers;

public class BloodDonationCentersControllerTests
{
    private readonly Mock<IBloodDonationService> _mockService;
    private readonly BloodDonationCentersController _controller;

    public BloodDonationCentersControllerTests()
    {
        _mockService = new Mock<IBloodDonationService>();
        _controller = new BloodDonationCentersController(_mockService.Object);
    }

    [Fact]
    public async Task GetBloodDonationCenters_ReturnsOkResult_WithListOfCenters()
    {
        // Arrange
        Guid centerId = Guid.NewGuid();
        var centers = new List<BloodDonationCenter> { new BloodDonationCenter { Id = centerId, Name = "Center 1" } };
        _mockService.Setup(service => service.GetAllCenters(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(centers);

        // Act
        var result = await _controller.GetBloodDonationCenters();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<BloodDonationCenter>>(okResult.Value);
        Assert.Single(returnValue);
        Assert.Equal(centerId, returnValue[0].Id);
    }

    [Fact]
    public async Task GetBloodDonationCentersById_ReturnsOkResult_WhenCenterExists()
    {
        // Arrange
        var centerId = Guid.NewGuid();
        var center = new BloodDonationCenter { Id = centerId, Name = "Center 1" };
        _mockService.Setup(service => service.GetCenterById(centerId)).ReturnsAsync(center);

        // Act
        var result = await _controller.GetBloodDonationCentersById(centerId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<BloodDonationCenter>(okResult.Value);
        Assert.Equal(centerId, returnValue.Id);
    }

    [Fact]
    public async Task GetBloodDonationCentersById_ReturnsNotFound_WhenCenterDoesNotExist()
    {
        // Arrange
        _mockService.Setup(service => service.GetCenterById(It.IsAny<Guid>())).ReturnsAsync((BloodDonationCenter)null);

        // Act
        var result = await _controller.GetBloodDonationCentersById(Guid.NewGuid());

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task AddBloodDonationCenter_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var centerDTO = new BloodDonationCenterDTO { Name = "New Center", Country = "Country", City = "City", Address = "Address", Latitude = 0, Longitude = 0 };
        var center = new BloodDonationCenter { Id = Guid.NewGuid(), Name = "New Center" };
        _mockService.Setup(service => service.AddCenter(It.IsAny<BloodDonationCenter>())).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.AddBloodDonationCenter(centerDTO);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(_controller.GetBloodDonationCentersById), createdAtActionResult.ActionName);
    }

    [Fact]
    public async Task AddBloodDonationCenter_ReturnsBadRequest_WhenCenterDTOIsNull()
    {       
        // Act
        var result = await _controller.AddBloodDonationCenter(null);
        
        // Assert
        Assert.IsType<BadRequestResult>(result);
    }    

    [Fact]
    public async Task UpdateBloodDonationCenter_ReturnsNoContent()
    {
        // Arrange
        var centerDTO = new BloodDonationCenterDTO { Name = "Updated Center", Country = "Country", City = "City", Address = "Address", Latitude = 0, Longitude = 0 };
        _mockService.Setup(service => service.UpdateCenter(It.IsAny<BloodDonationCenter>())).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.UpdateBloodDonationCenter(Guid.NewGuid(), centerDTO);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteBloodDonationCenter_ReturnsNoContent_WhenCenterExists()
    {
        // Arrange
        var center = new BloodDonationCenter { Id = Guid.NewGuid(), Name = "Center" };
        _mockService.Setup(service => service.GetCenterById(It.IsAny<Guid>())).ReturnsAsync(center);
        _mockService.Setup(service => service.DeleteCenter(It.IsAny<Guid>())).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteBloodDonationCenter(center.Id);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteBloodDonationCenter_ReturnsNotFound_WhenCenterDoesNotExist()
    {
        // Arrange
        _mockService.Setup(service => service.GetCenterById(It.IsAny<Guid>())).ReturnsAsync((BloodDonationCenter)null);

        // Act
        var result = await _controller.DeleteBloodDonationCenter(Guid.NewGuid());

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }    
}

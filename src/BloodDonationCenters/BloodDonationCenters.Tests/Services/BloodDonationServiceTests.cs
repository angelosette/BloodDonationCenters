using BloodDonationCenters.Application.Services;
using BloodDonationCenters.Domain.Entities;
using BloodDonationCenters.Infraestructure.Interfaces;
using FluentAssertions;
using Moq;
using System.Linq.Expressions;

namespace BloodDonationCenters.Tests.Services;

public class BloodDonationServiceTests
    {
    private readonly Mock<IRepository<BloodDonationCenter>> _repositoryMock;
    private readonly BloodDonationService _service;

    public BloodDonationServiceTests()
    {
        _repositoryMock = new Mock<IRepository<BloodDonationCenter>>();
        _service = new BloodDonationService(_repositoryMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllResults()
    {
        //Arrange
        var centers = new List<BloodDonationCenter>
        {
            new BloodDonationCenter { Country = "Spain", City = "Madrid" },
            new BloodDonationCenter { Country = "Spain", City = "Barcelona" },
            new BloodDonationCenter { Country = "France", City = "Paris" },
            new BloodDonationCenter { Country = "France", City = "Lyon" },
        };

        _repositoryMock
            .Setup(x => x.GetAll(It.IsAny<Expression<Func<BloodDonationCenter, bool>>>()))
            .ReturnsAsync((Expression<Func<BloodDonationCenter, bool>> predicate) => centers.Where(predicate.Compile()));

        // Act
        var result = await _service.GetAllCenters(null, null);

        // Assert
        Assert.Equal(4, result.Count());
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnFilteredResults()
    {

        //Arrange
        var centers = new List<BloodDonationCenter>
        {
            new BloodDonationCenter { Country = "Spain", City = "Madrid" },
            new BloodDonationCenter { Country = "Spain", City = "Barcelona" },
            new BloodDonationCenter { Country = "France", City = "Paris" },
            new BloodDonationCenter { Country = "France", City = "Lyon" },
        };

        _repositoryMock
            .Setup(x => x.GetAll(It.IsAny<Expression<Func<BloodDonationCenter, bool>>>()))
            .ReturnsAsync((Expression<Func<BloodDonationCenter, bool>> predicate) => centers.Where(predicate.Compile()));

        // Act
        var result = await _service.GetAllCenters("Spain", "Madrid");

        // Assert
        Assert.Single(result);
        Assert.Equal("Spain", result.First().Country);
        Assert.Equal("Madrid", result.First().City);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnCenter_WhenExists()
    {
        //Arrange
        var center = new BloodDonationCenter { Id = Guid.NewGuid() };
        _repositoryMock
            .Setup(x => x.GetById(center.Id))
            .ReturnsAsync(center);

        // Act
        var result = await _service.GetCenterById(center.Id);

        // Assert
        Assert.Equal(center.Id, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenNotFound()
    {
        //Arrange
        _repositoryMock
            .Setup(x => x.GetById(It.IsAny<Guid>()))
            .ReturnsAsync((BloodDonationCenter?)null);

        // Act
        var result = await _service.GetCenterById(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_ShouldCallRepositoryOnce()
    {
        //Arrange
        var center = new BloodDonationCenter();

        // Act
        await _service.AddCenter(center);

        // Assert
        _repositoryMock.Verify(x => x.Add(center), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldCallDelete_WhenExists()
    {
        //Arrange
        var center = new BloodDonationCenter { Id = Guid.NewGuid() };
        _repositoryMock
            .Setup(x => x.GetById(center.Id))
            .ReturnsAsync(center);


        // Act
        await _service.DeleteCenter(center.Id);

        // Assert
        _repositoryMock.Verify(x => x.Delete(center), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowKeyNotFoundException_WhenCenterNotExists()
    {
        //Arrange
        _repositoryMock
            .Setup(x => x.GetById(It.IsAny<Guid>()))
            .ReturnsAsync((BloodDonationCenter?)null);

        var centerId = Guid.NewGuid();

        // Act        
        Func<Task> act = async () => await _service.DeleteCenter(centerId);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Blood center with ID {centerId} was not found.");        
        _repositoryMock.Verify(x => x.Delete(It.IsAny<BloodDonationCenter>()), Times.Never);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdate_WhenCenterExists()
    {
        //Arrange
        var centerId = Guid.NewGuid();
        var existingCenter = new BloodDonationCenter
        {
            Id = centerId,
            Name = "Old Name"
        };
        var updatedCenter = new BloodDonationCenter
        {
            Id = centerId,
            Name = "New Name"
        };

        _repositoryMock
            .Setup(x => x.GetById(centerId))
            .ReturnsAsync(existingCenter);

        //act
        await _service.UpdateCenter(updatedCenter);        

        //assert
        _repositoryMock.Verify(x => x.Update(existingCenter), Times.Once);
        existingCenter.Name.Should().Be(updatedCenter.Name);
    }


    [Fact]
    public async Task UpdateAsync_ShouldThrowKeyNotFoundException_WhenCenterNotExists()
    {
        //Arrange        
        var updatedCenter = new BloodDonationCenter
        {
            Id = Guid.NewGuid(),
            Name = "New Name"
        };

        _repositoryMock
            .Setup(x => x.GetById(It.IsAny<Guid>()))
            .ReturnsAsync((BloodDonationCenter?)null);

        //act        
        Func<Task> act = async () => await _service.UpdateCenter(updatedCenter);

        //assert        
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Blood center with ID {updatedCenter.Id} was not found.");
        _repositoryMock.Verify(x => x.Update(It.IsAny<BloodDonationCenter>()), Times.Never);
    }
}

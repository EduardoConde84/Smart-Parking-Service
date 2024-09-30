using AutoMapper;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Moq;
using SmartParkingLot.Domain.Dtos;
using SmartParkingLot.Domain.Helpers;
using SmartParkingLot.Domain.Interfaces;
using SmartParkingLot.Domain.Mappings;
using SmartParkingLot.Domain.Services;
using SmartParkingLot.Domain.Tests.TestData;
using SmartParkingLot.Infrastructure.Context.Entities;
using SmartParkingLot.Infrastructure.Interfaces;

namespace SmartParkingLot.Domain.Tests.Services;

public class ParkingSpotServiceTests
{
    private readonly Mock<IParkingSpotsRepository> _parkingSpotsRepository;
    private readonly IParkingSpotsService _parkingSpotsService;
    private static IMapper _mapper;

    public ParkingSpotServiceTests()
    {

        if (_mapper == null)
        {
            var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new DomainToDtoMappingProfile());
        });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
        }
        _parkingSpotsRepository = new Mock<IParkingSpotsRepository>();
        _parkingSpotsService = new ParkingSpotService(_mapper, _parkingSpotsRepository.Object);
    }

    [Fact]
    public async void Should_create_ParkingSpot()
    {
        // Act

        var result = await _parkingSpotsService.CreateSpotAsync();

        // Assert

        _parkingSpotsRepository.Verify(x => x.InsertAsync(It.IsAny<ParkingSpots>()), Times.Once());
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public async void Should_Return_Failure_When_Id_InvalidAsync(int id)
    {
        // Arrange
        _parkingSpotsRepository.Setup(x => x.GetByIdAsync(id)).Returns(Task.FromResult(null as ParkingSpots));

        // Act
        var result = await _parkingSpotsService.DeleteSpotAsync(id);

        // Assert
        Assert.True(result.IsFailure);

    }

    [Fact]
    public async void Should_Return_Failure_When_Delete_Fails()
    {
        // Arrange
        var parkingSpotsTest = new ParkingSpots() { Id = 1, IsAvailable = true };
        _parkingSpotsRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(parkingSpotsTest);
        _parkingSpotsRepository.Setup(x => x.DeleteAsync(parkingSpotsTest)).ReturnsAsync(false);
        // Act
        var result = await _parkingSpotsService.DeleteSpotAsync(1);

        // Assert
        Assert.True(result.IsFailure);

    }

    [Fact]
    public async void Should_delete_ParkingSpot()
    {
        // Arrange
        var parkingSpotsTest = new ParkingSpots() { Id = 1, IsAvailable = true };
        _parkingSpotsRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(parkingSpotsTest);
        _parkingSpotsRepository.Setup(x => x.DeleteAsync(parkingSpotsTest)).ReturnsAsync(true);

        // Act
        var result = await _parkingSpotsService.DeleteSpotAsync(1);

        // Assert
        _parkingSpotsRepository.Verify(x => x.DeleteAsync(It.IsAny<ParkingSpots>()), Times.Once());
        Assert.True(result.IsSuccess);
    }

    [Theory]
    [ClassData(typeof(ParkingSpotServiceTestData))]
    public async void Spot_Cant_Be_Already_Occupied_Or_Free(
        ParkingSpotDto parkingSpotDto,
        ParkingSpots parkingSpots,
        Result result)
    {
        // Arrange
        _parkingSpotsRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(parkingSpots);
        _parkingSpotsRepository.Setup(x => x.UpdateAsync(It.IsAny<ParkingSpots>())).ReturnsAsync(true);

        // Act
        var testResult = await _parkingSpotsService.ManageSpotAsync(1, parkingSpotDto);

        // Assert
        Assert.Equal(result.IsSuccess, testResult.IsSuccess);
    }

    [Fact]
    public async void Should_Get_All_Results()
    {
        // Arrange
        var parkingSpotsList = new List<ParkingSpots>()
        {
            new()
            {
                Id = 1,
                IsAvailable = true,
            },
            new()
            {
                Id = 2,
                IsAvailable = false,
            }
        };

        _parkingSpotsRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(parkingSpotsList);

        // Act
        var result = await _parkingSpotsService.GetAllParkingSpotsAsync() as List<ParkingSpotDto>;

        // Assert
        Assert.Equal(parkingSpotsList.Count, result?.Count);
        Assert.Equal(parkingSpotsList[0].Id, result?[0].Id);
        Assert.Equal(parkingSpotsList[1].IsAvailable, result?[1].IsAvailable);
    }
}

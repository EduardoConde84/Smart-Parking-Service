using SmartParkingLot.Domain.Dtos;
using SmartParkingLot.Domain.Helpers;
using SmartParkingLot.Infrastructure.Context.Entities;

namespace SmartParkingLot.Domain.Tests.TestData;

public class ParkingSpotServiceTestData : TheoryData<ParkingSpotDto, ParkingSpots, Result>
{
    public ParkingSpotServiceTestData()
    {
        Add(new ParkingSpotDto() { Id = 1, IsAvailable = true },
         new ParkingSpots() { Id = 1, IsAvailable = true },
         Result.Failure(ParkingSpotErrors.AlreadyFree(1)));

        Add(new ParkingSpotDto() { Id = 1, IsAvailable = false },
         new ParkingSpots() { Id = 1, IsAvailable = false },
         Result.Failure(ParkingSpotErrors.AlreadyOccupied(1)));

        Add(new ParkingSpotDto() { Id = 1, IsAvailable = true },
         new ParkingSpots() { Id = 1, IsAvailable = false },
         Result.Success());
    }
}

using System;

namespace SmartParkingLot.Domain.Helpers;

public static class ParkingSpotErrors
{
    public static Error AlreadyOccupied(int id) => new(
        "ParkingSpot.AlreadyOccupied", $"The parking spot with number {id} is already occupied");
    public static Error AlreadyFree(int id) => new(
            "ParkingSpot.AlreadyFree", $"The parking spot with number {id} is already free");
    public static Error NotFound(int id) => new(
        "ParkingSpot.NotFound", $"The parking spot with number {id} don't exist");
    public static Error DatabaseError(int id) => new(
        "ParkingSpot.DatabaseError", $"The parking spot with number {id} couldn't be updated");
}

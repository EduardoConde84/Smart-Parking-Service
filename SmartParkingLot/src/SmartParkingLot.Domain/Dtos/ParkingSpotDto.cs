using System.ComponentModel;

namespace SmartParkingLot.Domain.Dtos;

public class ParkingSpotDto
{
    public int Id { get; set; }

    [DisplayName("IsAvailable")]
    public bool IsAvailable { get; set; }

}

using SmartParkingLot.Domain.Dtos;
using SmartParkingLot.Domain.Helpers;

namespace SmartParkingLot.Domain.Interfaces
{
    public interface IParkingSpotsService
    {
        Task<IEnumerable<ParkingSpotDto>> GetAllParkingSpotsAsync();
        Task<Result> ManageSpotAsync(int id, ParkingSpotDto parkingSpotDto);
        Task<Result> DeleteSpotAsync(int id);
        Task<bool> CreateSpotAsync();
    }
}
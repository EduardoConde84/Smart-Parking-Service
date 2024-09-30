using AutoMapper;
using SmartParkingLot.Domain.Dtos;
using SmartParkingLot.Domain.Helpers;
using SmartParkingLot.Domain.Interfaces;
using SmartParkingLot.Infrastructure.Context.Entities;
using SmartParkingLot.Infrastructure.Interfaces;

namespace SmartParkingLot.Domain.Services
{
    public class ParkingSpotService : IParkingSpotsService
    {
        private readonly IMapper _mapper;
        private readonly IParkingSpotsRepository _parkingSpotRepository;

        public ParkingSpotService(IMapper mapper, IParkingSpotsRepository parkingSpotRepository)
        {
            _mapper = mapper;
            _parkingSpotRepository = parkingSpotRepository;
        }
        public async Task<bool> CreateSpotAsync()
        {
            var parkingSpotItem = new ParkingSpotDto() { IsAvailable = true };

            var result = await _parkingSpotRepository.InsertAsync(_mapper.Map<ParkingSpots>(parkingSpotItem));

            return result;
        }

        public async Task<Result> DeleteSpotAsync(int id)
        {
            var parkingSpotItem = await _parkingSpotRepository.GetByIdAsync(id);

            if (parkingSpotItem == null) return Result.Failure(ParkingSpotErrors.NotFound(id));

            var result = await _parkingSpotRepository.DeleteAsync(parkingSpotItem);

            if (result == false) return Result.Failure(ParkingSpotErrors.DatabaseError(id));

            return Result.Success();
        }

        public async Task<IEnumerable<ParkingSpotDto>> GetAllParkingSpotsAsync()
        {
            var result = await _parkingSpotRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ParkingSpotDto>>(result);
        }

        public async Task<Result> ManageSpotAsync(int id, ParkingSpotDto parkingSpotDto)
        {
            var parkingSpot = await _parkingSpotRepository.GetByIdAsync(id);

            if (parkingSpot == null) return Result.Failure(ParkingSpotErrors.NotFound(id));

            if (!parkingSpot.IsAvailable && !parkingSpotDto.IsAvailable)
            {
                return Result.Failure(ParkingSpotErrors.AlreadyOccupied(id));
            }
            else if (parkingSpot.IsAvailable && parkingSpotDto.IsAvailable)
            {
                return Result.Failure(ParkingSpotErrors.AlreadyFree(id));
            }

            var result = await _parkingSpotRepository.UpdateAsync(_mapper.Map<ParkingSpots>(parkingSpotDto));

            if (result == false) return Result.Failure(ParkingSpotErrors.DatabaseError(id));

            return Result.Success();
        }
    }
}
using AutoMapper;
using SmartParkingLot.Domain.Dtos;
using SmartParkingLot.Domain.Helpers;
using SmartParkingLot.Domain.Interfaces;
using SmartParkingLot.Infrastructure.Interfaces;

namespace SmartParkingLot.Domain.Services
{
    public class DevicesService : IDevicesService
    {
        private readonly IDevicesRepository _devicesRepository;

        public DevicesService(IDevicesRepository devicesRepository)
        {
            _devicesRepository = devicesRepository;
        }
        public async Task<Result> GetDeviceRegistered(DevicesDto devicesDto)
        {
            var result = await _devicesRepository.GetByGuidAsync(devicesDto.DeviceAsignedNumber);
            if (result == null) return Result.Failure(DevicesErrors.NotRegistered(devicesDto.DeviceAsignedNumber));

            return Result.Success();
        }
    }
}
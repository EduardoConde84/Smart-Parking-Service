using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartParkingLot.Domain.Dtos;
using SmartParkingLot.Domain.Helpers;

namespace SmartParkingLot.Domain.Interfaces
{
    public interface IDevicesService
    {
        Task<Result> GetDeviceRegistered(DevicesDto devicesDto);
    }
}
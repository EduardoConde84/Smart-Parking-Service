using SmartParkingLot.Domain.Helpers;
using SmartParkingLot.Infrastructure.Context.Entities;

namespace SmartParkingLot.Domain.Tests.TestData;

public class DeviceServiceTestData : TheoryData<Devices, Result>
{
    public DeviceServiceTestData()
    {
        Add(new Devices() { Id = 1, DeviceAsignedNumber = new Guid() }, Result.Success());
        Add(null as Devices, Result.Failure(DevicesErrors.NotRegistered(new Guid())));
    }
}

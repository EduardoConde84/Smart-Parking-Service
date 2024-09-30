using SmartParkingLot.Infrastructure.Context.Entities;

namespace SmartParkingLot.Infrastructure.Interfaces
{
    public interface IDevicesRepository : IRepository<Devices>
    {
        Task<Devices?> GetByGuidAsync(Guid? id);
    }
}
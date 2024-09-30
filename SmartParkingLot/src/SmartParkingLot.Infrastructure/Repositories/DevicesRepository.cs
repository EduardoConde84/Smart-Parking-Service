using Microsoft.EntityFrameworkCore;
using SmartParkingLot.Infrastructure.Context;
using SmartParkingLot.Infrastructure.Context.Entities;
using SmartParkingLot.Infrastructure.Interfaces;

namespace SmartParkingLot.Infrastructure.Repositories
{
    public class DevicesRepository : Repository<Devices>, IDevicesRepository
    {
        private readonly AplicationDbContext _context;
        public DevicesRepository(AplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public virtual async Task<Devices?> GetByGuidAsync(Guid? id)
            => await _context.Set<Devices>().AsNoTracking().SingleOrDefaultAsync(s => s.DeviceAsignedNumber == id);
    }
}
using Microsoft.EntityFrameworkCore;
using SmartParkingLot.Infrastructure.Context;
using SmartParkingLot.Infrastructure.Context.Entities;
using SmartParkingLot.Infrastructure.Interfaces;

namespace SmartParkingLot.Infrastructure.Repositories
{
    public class ParkingSpotsRepository : Repository<ParkingSpots>, IParkingSpotsRepository
    {
        public ParkingSpotsRepository(AplicationDbContext context) : base(context)
        {
        }
    }
}
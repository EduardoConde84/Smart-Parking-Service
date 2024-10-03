using Microsoft.EntityFrameworkCore;
using SmartParkingLot.Infrastructure.Context;
using SmartParkingLot.Infrastructure.Context.Entities;
using SmartParkingLot.Infrastructure.Interfaces;

namespace SmartParkingLot.Infrastructure.Repositories
{
    public class ParkingSpotsRepository : Repository<ParkingSpots>, IParkingSpotsRepository
    {
        private readonly AplicationDbContext _context;

        public ParkingSpotsRepository(AplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<ParkingSpots> GetAllParkingSpotsPaginated()
        {
            throw new NotImplementedException();
        }
    }
}
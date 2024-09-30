using Microsoft.EntityFrameworkCore;
using SmartParkingLot.Infrastructure.Context.Entities;

namespace SmartParkingLot.Infrastructure.Context;

public class AplicationDbContext : DbContext
{
    public DbSet<ParkingSpots> ParkingSpots { get; set; } = null!;
    public DbSet<Devices> Devices { get; set; } = null!;
    public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Devices>().HasData(new Devices() { Id = 1, DeviceAsignedNumber = Guid.NewGuid() });
        modelBuilder.Entity<Devices>().HasData(new Devices() { Id = 2, DeviceAsignedNumber = Guid.NewGuid() });
        modelBuilder.Entity<ParkingSpots>().HasData(new ParkingSpots() { Id = 1, IsAvailable = true });
        modelBuilder.Entity<ParkingSpots>().HasData(new ParkingSpots() { Id = 2, IsAvailable = true });
        base.OnModelCreating(modelBuilder);
    }
}

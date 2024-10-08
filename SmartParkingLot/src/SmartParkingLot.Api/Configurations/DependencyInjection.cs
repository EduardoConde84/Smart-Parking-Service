using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartParkingLot.Domain.Helpers.Pagination;
using SmartParkingLot.Domain.Interfaces;
using SmartParkingLot.Domain.Mappings;
using SmartParkingLot.Domain.Models;
using SmartParkingLot.Domain.Services;
using SmartParkingLot.Infrastructure.Context;
using SmartParkingLot.Infrastructure.Interfaces;
using SmartParkingLot.Infrastructure.Repositories;

namespace SmartParkingLot.Api.Configurations
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureApi(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SQLiteDefault");

            services.AddDbContext<AplicationDbContext>(options => options.UseSqlite(connectionString), ServiceLifetime.Scoped);
            services.Configure<PaginationOptions>(configuration.GetSection("Pagination"));
            services.AddScoped<IParkingSpotsRepository, ParkingSpotsRepository>();
            services.AddScoped<IDevicesRepository, DevicesRepository>();
            services.AddScoped<IParkingSpotsService, ParkingSpotService>();
            services.AddScoped<IDevicesService, DevicesService>();
            services.AddScoped<IPaginationFilter, PaginationFilter>();
            services.AddScoped<IPaginationHelper, PaginationHelper>();
            
            services.AddAutoMapper(typeof(DomainToDtoMappingProfile));
        }
    }
}
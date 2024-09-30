
using AutoMapper;
using SmartParkingLot.Domain.Dtos;
using SmartParkingLot.Infrastructure.Context.Entities;

namespace SmartParkingLot.Domain.Mappings
{
    public class DomainToDtoMappingProfile : Profile
    {
        public DomainToDtoMappingProfile()
        {
            CreateMap<ParkingSpots, ParkingSpotDto>().ReverseMap();
            CreateMap<Devices, DevicesDto>().ReverseMap();
        }
    }
}
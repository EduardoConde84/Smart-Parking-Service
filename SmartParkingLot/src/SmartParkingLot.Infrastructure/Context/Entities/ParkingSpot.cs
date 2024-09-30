using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SmartParkingLot.Infrastructure.Context.Entities
{
    [Table("ParkingSpots")]
    public class ParkingSpots : BaseEntity
    {
        [Column("IsAvailable")]
        public bool IsAvailable { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartParkingLot.Infrastructure.Context.Entities
{
    [Table("Devices")]
    public class Devices : BaseEntity
    {
        [Column("DeviceAsignedNumber")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid DeviceAsignedNumber { get; set; }
    }
}
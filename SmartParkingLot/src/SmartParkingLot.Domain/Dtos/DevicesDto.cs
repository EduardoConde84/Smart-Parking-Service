using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartParkingLot.Domain.Dtos
{
    public class DevicesDto
    {
        public int Id { get; set; }

        [RegularExpression("^((?!00000000-0000-0000-0000-000000000000).)*$", ErrorMessage = "Cannot use default Guid")]
        [DisplayName("DeviceAsignedNumber")]
        public Guid DeviceAsignedNumber { get; set; }
    }
}
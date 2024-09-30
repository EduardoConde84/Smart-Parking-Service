using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartParkingLot.Domain.Helpers
{
    public static class DevicesErrors
    {
        public static Error NotRegistered(Guid id) => new(
        "Devices.NotRegistered", $"The device with Id '{id}' is not registered");
    }
}
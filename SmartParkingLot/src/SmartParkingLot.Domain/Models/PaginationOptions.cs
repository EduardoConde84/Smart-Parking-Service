using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartParkingLot.Domain.Models
{
    public sealed class PaginationOptions
    {
        public int DefaultPageNumber { get; set; } = 1;

        public int DefaultPageSize { get; set; } = 10;

        public int MaxPageSize { get; set; } = 50;
    }
}
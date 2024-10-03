using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartParkingLot.Domain.Interfaces
{
    public interface IPaginationFilter
    {
    int PageNumber { get; set; }

    int PageSize { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartParkingLot.Domain.Models
{
    public sealed record PaginationFilterRequest(int PageNumber, int PageSize);
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartParkingLot.Domain.Helpers.Pagination
{
    public sealed class PagedData<T>
    {
    public List<T> Data { get; set; } = [];
    public int TotalRecords { get; set; }
    }
}
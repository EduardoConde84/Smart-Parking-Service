using SmartParkingLot.Domain.Models;

namespace SmartParkingLot.Domain.Interfaces
{
    public interface IPaginationHelper
    {
        PagedList<IEnumerable<T>> CreatePagedResponse<T>(
        IEnumerable<T> data,
        int pageNumber,
        int pageSize);
    }
}
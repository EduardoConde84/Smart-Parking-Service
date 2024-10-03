using SmartParkingLot.Domain.Interfaces;
using SmartParkingLot.Domain.Models;

namespace SmartParkingLot.Domain.Helpers.Pagination
{
    public sealed class PaginationHelper(IPaginationFilter paginationFilter) : IPaginationHelper
    {
        private readonly IPaginationFilter _paginationFilter = paginationFilter;

        public PagedList<IEnumerable<T>> CreatePagedResponse<T>(IEnumerable<T> data, int pageNumber, int pageSize)
        {
            _paginationFilter.PageNumber = pageNumber;
            _paginationFilter.PageSize = pageSize;

            var filteredData = data
                .Skip((_paginationFilter.PageNumber - 1) * _paginationFilter.PageSize)
                .Take(_paginationFilter.PageSize)
                .ToList();

            var pagedData = new PagedData<T>
            {
                Data = filteredData,
                TotalRecords = filteredData.Count
            };

            return CreatePagedResponse(filteredData, pagedData.TotalRecords);
        }

        private PagedList<IEnumerable<T>> CreatePagedResponse<T>(IEnumerable<T> pagedData, int totalCount) =>
        new(pagedData, totalCount, _paginationFilter.PageNumber, _paginationFilter.PageSize);
    }
}
using Microsoft.Extensions.Options;
using SmartParkingLot.Domain.Interfaces;
using SmartParkingLot.Domain.Models;

namespace SmartParkingLot.Domain.Helpers.Pagination
{
    public class PaginationFilter : IPaginationFilter
    {
        private readonly PaginationOptions _paginationOptions;
        private int _pageNumber;
        private int _pageSize;

        public PaginationFilter(IOptions<PaginationOptions> paginationOptions)
        {
            _paginationOptions = paginationOptions.Value;
            SetDefaultValues();
        }

        public PaginationFilter(IOptions<PaginationOptions> paginationOptions, int pageNumber, int pageSize)
        {
            _paginationOptions = paginationOptions.Value;
            PageNumber = pageNumber == 0 ? _paginationOptions.DefaultPageNumber : pageNumber;
            PageSize = pageSize == 0 ? _paginationOptions.DefaultPageSize : pageSize;
        }

        public int PageNumber
        {
            get => _pageNumber;
            set => _pageNumber = value <= 0 ? _paginationOptions.DefaultPageNumber : value;
        }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value <= 0 || value > _paginationOptions.MaxPageSize) ? _paginationOptions.DefaultPageSize : value;
        }

        private void SetDefaultValues()
        {
            _pageNumber = _paginationOptions.DefaultPageNumber;
            _pageSize = _paginationOptions.DefaultPageSize;
        }
    }
}

namespace SmartParkingLot.Domain.Models
{
    public class PagedList<T>(T data, int count, int pageNumber, int pageSize)
    {
        public T? Data { get; set; } = data ?? throw new ArgumentNullException(nameof(data));
        public int CurrentPage { get; set; } = pageNumber;
        public int TotalPages { get; set; } = (int)Math.Ceiling(count / (double)pageSize);
        public int PageSize { get; set; } = pageSize;
        public int TotalCount { get; set; } = count;
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
    }
}
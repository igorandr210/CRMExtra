using System.Collections.Generic;

namespace Application.Common.DTOs
{
    public class PaginatedDataDto<T>
    {
        public List<T> Data { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
    }
}

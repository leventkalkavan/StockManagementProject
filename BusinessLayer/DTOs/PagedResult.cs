using System;
using System.Collections.Generic;

namespace BusinessLayer.DTOs
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new();
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public string? SearchTerm { get; set; }

        public int TotalPages => PageSize <= 0 ? 0 : (int)Math.Ceiling((decimal)TotalCount / PageSize);
        public bool HasPrevious => Page > 1;
        public bool HasNext => Page < TotalPages;
    }
}

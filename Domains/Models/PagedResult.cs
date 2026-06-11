using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.Models
{
    public class PagedResult<T>(IEnumerable<T> items, int count, int pageNumber, int pageSize)
    {
        public IEnumerable<T> Items { get; init; } = items;
        public int PageNumber { get; init; } = pageNumber;
        public int PageSize { get; init; } = pageSize;
        public int TotalCount { get; init; } = count;
        public int TotalPages { get; init; } = (int)Math.Ceiling(count / (double)pageSize);
        public bool HasPrevious => PageNumber > 1;
        public bool HasNext => PageNumber < TotalPages;
    }
}

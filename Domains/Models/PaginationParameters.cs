using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.Models
{
    public class PaginationParameters
    {
        private const int MaxPageSize = 100;
        private int _pageSize = 10;


        public int PageNumber { get; init; } = 1;
        public int TotalCount { get; init; }

        public int PageSize
        {
            get => _pageSize;
            init => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
    }
}

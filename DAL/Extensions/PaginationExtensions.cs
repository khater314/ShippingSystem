using Domains.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Extensions
{
    public static class PaginationExtensions
    {
        public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
            this IQueryable<T> query,
            PaginationParameters parameters,
            CancellationToken cancellationToken = default)
        {
            // 1. Get total count before partitioning the data
            var totalCount = await query.CountAsync(cancellationToken);

            // 2. Fetch the specific page items from the database
            var items = await query
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync(cancellationToken);

            // 3. Return the wrapped result
            return new PagedResult<T>(items, totalCount, parameters.PageNumber, parameters.PageSize);
        }
    }
}

using Domains;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DAL.Contracts
{
    public interface IViewRepository<T> where T : notnull, BaseView
    {
        //Task<IReadOnlyList<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default);
        Task<T> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, 
            CancellationToken ct = default);
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> filter, 
            CancellationToken ct = default);
    }
}

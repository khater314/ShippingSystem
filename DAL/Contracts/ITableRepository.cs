using Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DAL.Contracts
{
    public interface ITableRepository<T> where T : notnull, BaseEntity
    {
        //Task<IReadOnlyList<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default);
        Task<T> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task AddAsync(T entity, CancellationToken ct = default);
        Task<T> AddAndReturnAsync(T entity, CancellationToken ct = default);
        Task<Guid> AddAndGetIdAsync(T entity, CancellationToken ct = default);
        bool Add(T entity);
        bool Add(T entity, out Guid id);
        Task UpdateAsync(T entity, CancellationToken ct = default);
        Task DeleteAsync(Guid id, CancellationToken ct = default);
        Task ChangeStatusAsync(Guid id, int status = 1, CancellationToken ct = default);
        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, CancellationToken ct = default);
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> filter, CancellationToken ct = default);
        Task<List<TResult>> GetListAsync<TResult>(
            Expression<Func<T, bool>>? filter = null,
            Expression<Func<T, TResult>>? selector = null,
            Expression<Func<T, object>>? orderBy = null,
            bool isDescending = false,
            CancellationToken ct = default,
            params Expression<Func<T, object>>[] includers);
    }
}

using Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Contracts
{
    public interface IViewRepository<T> where T : notnull, BaseView
    {
        //Task<IReadOnlyList<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default);
        Task<T> GetByIdAsync(Guid id, CancellationToken ct = default);
    }
}

using Domains.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Contracts
{
    public interface IUnitOfWork
    {
        ITableRepository<T> GetRepository<T>() where T : BaseEntity;
        Task BeginTransactionAsync(CancellationToken ct = default);
        Task<bool> CommitAsync(CancellationToken ct = default);
        Task RollbackAsync(CancellationToken ct = default);
        ValueTask DisposeAsync();
    }
}

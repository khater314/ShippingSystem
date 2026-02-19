using DAL.Contracts;
using Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Contracts
{
    public interface IBaseService<T, DTO>
    {
        Task<IEnumerable<DTO>> GetAllAsync(CancellationToken ct = default);
        Task<DTO> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task AddAsync(DTO entity, Guid userId, CancellationToken ct = default);
        Task UpdateAsync(DTO entity, Guid userId, CancellationToken ct = default);
        Task ChangeStatusAsync(Guid id, Guid userId, int status = 1, CancellationToken ct = default);
    }
}

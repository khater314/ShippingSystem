using DAL.DbContext;
using Microsoft.EntityFrameworkCore;
using Domains;
using DAL.Contracts;
using Microsoft.Extensions.Logging;
using DAL.Exceptions;

namespace DAL.Repositories
{
    public class TableRepository<T>(ShippingContext _context, ILogger<TableRepository<T>> _logger)
        : ITableRepository<T> where T : notnull, BaseEntity
    {
        private readonly DbSet<T> _dbSet = _context.Set<T>();

        #region Private Handling Method
        private async Task<TResult> ExecuteWithHandlingAsync<TResult>(Func<Task<TResult>> action, string errorMessage)
        {
            try
            {
                return await action();
            }
            catch (InvalidOperationException ex) //
            {
                _logger.LogWarning(ex, "Entity not found - {Message}", errorMessage);
                throw new DataAccessException("The requested record was not found in the system.", ex);
            }
            catch (DbUpdateException ex) //
            {
                _logger.LogError(ex, "Database update failed - {Message}", errorMessage);
                throw new DataAccessException("A database constraint error occurred. Please check your data.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error - {Message}", errorMessage);
                throw new DataAccessException(errorMessage, ex);
            }
        }
        #endregion
        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default)
        {
            return await ExecuteWithHandlingAsync(async () =>
            {
                return await _dbSet.Where(i => i.CurrentState == 1).ToListAsync(ct);
            }, "Error retrieving all records.");
        }
        public async Task<T> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await ExecuteWithHandlingAsync(async () =>
            {
                return await _dbSet.Where(i => i.CurrentState == 1).FirstAsync(e => e.Id == id, ct);
            }, $"Error retrieving record with ID: {id}");
        }

        public async Task AddAsync(T entity, CancellationToken ct = default)
        {
            await ExecuteWithHandlingAsync<object>(async () =>
            {
                await _dbSet.AddAsync(entity, ct);
                await _context.SaveChangesAsync(ct);
                return null!;
            }, "Failed to add a new record.");
        }

        public async Task UpdateAsync(T entity, CancellationToken ct = default)
        {
            await ExecuteWithHandlingAsync<object>(async () =>
            {
                entity.CurrentState = 1;
                _dbSet.Update(entity);
                await _context.SaveChangesAsync(ct);
                return null!;
            }, "Failed to update the record.");
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            await ExecuteWithHandlingAsync<object>(async () =>
            {
                var entity = await GetByIdAsync(id, ct);
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync(ct);
                return null!;
            }, "Failed to delete the record.");
        }

        public async Task ChangeStatusAsync(Guid id, int status = 1, CancellationToken ct = default)
        {
            await ExecuteWithHandlingAsync<object>(async () =>
            {
                var entity = await GetByIdAsync(id, ct);
                entity.CurrentState = status;
                await UpdateAsync(entity, ct);
                return null!;
            }, "Failed to change record status.");
        }
    }
}

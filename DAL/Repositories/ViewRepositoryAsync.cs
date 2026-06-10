using DAL.Contracts;
using DAL.DbContext;
using DAL.Exceptions;
using Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public class ViewRepository<T>(ShippingContext _context, ILogger<ViewRepository<T>> _logger)
        : IViewRepository<T> where T : notnull, BaseView
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

        public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, CancellationToken ct = default)
        {
            return await ExecuteWithHandlingAsync(async () =>
            {
                return await _dbSet.Where(i => i.CurrentState == 1).FirstOrDefaultAsync(filter, ct);
            }, "Error retrieving the record.");
        }

        public async Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> filter, CancellationToken ct = default)
        {
            return await ExecuteWithHandlingAsync(async () =>
            {
                return await _dbSet.Where(i => i.CurrentState == 1).Where(filter).ToListAsync(ct);
            }, "Error retrieving the records.");
        }

    }
}

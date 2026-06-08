using DAL.DbContext;
using Microsoft.EntityFrameworkCore;
using Domains;
using DAL.Contracts;
using Microsoft.Extensions.Logging;
using DAL.Exceptions;
using System.Linq.Expressions;

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
        private TResult ExecuteWithHandling<TResult>(Func<TResult> action, string errorMessage)
        {
            try
            {
                return action();
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
            await ExecuteWithHandlingAsync<bool>(async () =>
            {
                await _dbSet.AddAsync(entity, ct);
                await _context.SaveChangesAsync(ct);
                return true;
            }, "Failed to add a new record.");
        }
        public async Task<Guid> AddAndGetIdAsync(T entity, CancellationToken ct = default)
        {
            return await ExecuteWithHandlingAsync<Guid>(async () =>
            {
                await _dbSet.AddAsync(entity, ct);
                await _context.SaveChangesAsync(ct);
                return entity.Id;
            }, "Failed to add a new record.");
        }
        public async Task<T> AddAndReturnAsync(T entity, CancellationToken ct = default)
        {
            return await ExecuteWithHandlingAsync<T>(async () =>
            {
                await _dbSet.AddAsync(entity, ct);
                await _context.SaveChangesAsync(ct);
                return entity;
            }, "Failed to add a new record.");
        }

        public bool Add(T entity)
        {
            return ExecuteWithHandling(() =>
            {
                _dbSet.Add(entity);
                _context.SaveChanges();
                return true;
            }, "Failed to add a new record.");
        }

        public bool Add(T entity, out Guid id)
        {

            ExecuteWithHandling<object?>(() =>
            {
                _dbSet.Add(entity);
                _context.SaveChanges();
                return null;
            }, "Failed to add a new record.");

            if (entity.Id == Guid.Empty)
            { id = Guid.Empty; return false; }

            id = entity.Id;
            return true;
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
                await _context.SaveChangesAsync(ct);
                return null!;
            }, "Failed to change record status.");
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

        public async Task<List<TResult>> GetListAsync<TResult>(
            Expression<Func<T, bool>>? filter = null,
            Expression<Func<T, TResult>>? selector = null,
            Expression<Func<T, object>>? orderBy = null,
            bool isDescending = false,
            CancellationToken ct = default,
            params Expression<Func<T, object>>[] includers)
        {
            return await ExecuteWithHandlingAsync(async () =>
            {

                IQueryable<T> query = _dbSet.AsNoTracking().Where(i => i.CurrentState == 1);

                // for JOINs in db
                if (includers != null && includers.Length > 0)
                {
                    foreach (var includeProperty in includers)
                    {
                        query = query.Include(includeProperty);
                    }
                }

                // Where
                if (filter != null)
                {
                    query = query.Where(filter);
                }

                // Order By
                if (orderBy != null)
                {
                    query = isDescending
                        ? query.OrderByDescending(orderBy)
                        : query.OrderBy(orderBy);
                }

                // Select
                if (selector != null)
                {
                    return await query.Select(selector).ToListAsync(ct);
                }

                // if selector is null, we assume TResult is T and cast the results
                return await query.Cast<TResult>().ToListAsync(ct);

            }, "Error retrieving records with custom query configurations.");
        }

    }
}

using DAL.Contracts;
using DAL.DbContext;
using Domains;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories
{
    public class UnitOfWork(ShippingContext context, ILoggerFactory logger) : IUnitOfWork
    {
        private readonly ShippingContext _context = context;
        private readonly Dictionary<Type, object> _repositories = []; 
        private IDbContextTransaction? _transaction;
        private readonly ILoggerFactory _logger = logger;

        public async Task BeginTransactionAsync(CancellationToken ct = default)
            => _transaction = await _context.Database.BeginTransactionAsync(ct);

        public async Task<bool> CommitAsync(CancellationToken ct = default)
        {
            try
            {
                await _context.SaveChangesAsync(ct);
                if (_transaction != null)
                    await _transaction.CommitAsync(ct);
                return true;
            }
            catch
            {
                await RollbackAsync(ct); 
                return false;
                throw;
            }
        }

        public async Task RollbackAsync(CancellationToken ct = default)
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync(ct);
                await _transaction.DisposeAsync();
                _transaction = null; 
            }
        }

        public ITableRepository<T> GetRepository<T>() where T : BaseEntity
        {
            if (!_repositories.TryGetValue(typeof(T), out var repo))
            {
                repo = new TableRepository<T>(
                    _context, 
                    _logger.CreateLogger<TableRepository<T>>()
                );
                _repositories[typeof(T)] = repo;
            }
            return (ITableRepository<T>)repo; 
        }

        public async ValueTask DisposeAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null; 
            }

            await _context.DisposeAsync();
        }
    }
}
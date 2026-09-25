using Microsoft.EntityFrameworkCore;
using Shared.Domain;

namespace Shared.Infrastructure
{
    public class DbFactoryBase<TContext> : IDbFactory<TContext> where TContext : DbContext
    {
        private bool _disposed;
        private Func<TContext> _instanceFunc;
        private TContext _context;
        public TContext Context => _context ?? (_context = _instanceFunc.Invoke());

        public DbFactoryBase(Func<TContext> dbContextFactory)
        {
            _context = dbContextFactory();
        }

        public void Dispose()
        {
            if (!_disposed && _context != null)
            {
                _disposed = true;
                _context.Dispose();
            }
        }
    }
}

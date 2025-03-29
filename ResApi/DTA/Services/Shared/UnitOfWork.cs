using RealesApi.DTA.Intefaces;
using RealesApi.Models;
using System.Threading;
using System.Threading.Tasks;

namespace RealesApi.DTA.Services.Shared
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }
        public Task Save(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}


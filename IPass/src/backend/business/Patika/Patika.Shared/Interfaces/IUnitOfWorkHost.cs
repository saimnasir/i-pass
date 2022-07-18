using System.Threading;
using System.Threading.Tasks;

namespace Patika.Shared.Interfaces
{
    public interface IUnitOfWorkHost
    {
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        void BeginTransaction();
        Task CommitAsync(CancellationToken cancellationToken = default);
        void Commit();
        Task RollbackAsync(CancellationToken cancellationToken = default);
        void Rollback();
    }
}
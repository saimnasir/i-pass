using Patika.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Patika.Shared.Interfaces
{
    public interface IGenericQueryRepository<T, U> where T : IEntity<U>
    {
        void SetMaxSelectCount(int count);
        Task<IEnumerable<T>> GetAllAsync(Pagination pagination = default, List<Sort> sorts = default , bool includeLogicalDeleted = false, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> selector, bool includeAll = false, Pagination pagination = default, List<Sort> sorts = default, bool includeLogicalDeleted = false, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> WhereAsync(IEnumerable<Condition> conditions, bool includeAll = false, Pagination pagination = default, List<Sort> sorts = default, bool includeLogicalDeleted = false, CancellationToken cancellationToken = default);
        Task<T> GetByIdAsync(U id, bool includeAll = false, bool includeLogicalDeleted = false, CancellationToken cancellationToken = default);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> selector = null, bool includeAll = false, bool includeLogicalDeleted = false, CancellationToken cancellationToken = default);
        Task<T> LastOrDefaultAsync(Expression<Func<T, bool>> selector = null, bool includeAll = false, bool includeLogicalDeleted = false, CancellationToken cancellationToken = default);
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> selector = null, bool includeAll = false, bool includeLogicalDeleted = false, CancellationToken cancellationToken = default);
        Task<T> SingleAsync(Expression<Func<T, bool>> selector = null, bool includeAll = false, bool includeLogicalDeleted = false, CancellationToken cancellationToken = default);
        Task<int> CountAsync(Expression<Func<T, bool>> selector = null, bool includeLogicalDeleted = false, CancellationToken cancellationToken = default);
        Task<bool> AnyAsync(Expression<Func<T, bool>> selector = null, bool includeLogicalDeleted = false, CancellationToken cancellationToken = default);
    }
}
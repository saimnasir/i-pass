using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Patika.Shared.Interfaces
{
    public interface IGenericCUDRepository<T, U> where T : IEntity<U>
    {
        Task<T> InsertOneAsync(T entity, IUnitOfWorkHostInterface unitOfWorkHost = null, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> InsertManyAsync(IEnumerable<T> entities, IUnitOfWorkHostInterface unitOfWorkHost = null, CancellationToken cancellationToken = default);
        Task<T> UpdateOneAsync(T entity, IUnitOfWorkHostInterface unitOfWorkHost = null, CancellationToken cancellationToken = default, Expression<Func<T, object>> includes = null);
        Task<IEnumerable<T>> UpdateManyAsync(IEnumerable<T> entities, IUnitOfWorkHostInterface unitOfWorkHost = null, CancellationToken cancellationToken = default, Expression<Func<T, object>> includes = null);
        Task DeleteOneAsync(U id, IUnitOfWorkHostInterface unitOfWorkHost = null, CancellationToken cancellationToken = default);
        Task DeleteManyAsync(IEnumerable<T> entities, IUnitOfWorkHostInterface unitOfWorkHost = null, CancellationToken cancellationToken = default);
    }
}
using System;

namespace Patika.Shared.Interfaces
{
    public interface IQueryRepository<T> : IGenericQueryRepository<T, Guid> where T : IEntity<Guid>
    {
    }
}
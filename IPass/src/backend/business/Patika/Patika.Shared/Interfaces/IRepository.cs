using System;

namespace Patika.Shared.Interfaces
{
    public interface IRepository<T> : IGenericRepository<T, Guid> where T : IEntity<Guid>
    { }
}
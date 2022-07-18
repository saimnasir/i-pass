using System;

namespace Patika.Shared.Interfaces
{
    public interface ICUDRepository<T> : IGenericCUDRepository<T, Guid> where T : IEntity<Guid>
    {
    }
}
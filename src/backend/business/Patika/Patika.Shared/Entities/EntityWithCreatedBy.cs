using Patika.Shared.Interfaces;
using System;

namespace Patika.Shared.Entities
{
    public abstract class EntityWithCreatedBy<T> : IEntity<T>, IHasCreatedBy
    {
        public Guid CreatedBy { get; set; }
        public T Id { get; set; }
    }

    public abstract class EntityWithCreatedBy : EntityWithCreatedBy<Guid> { }
}
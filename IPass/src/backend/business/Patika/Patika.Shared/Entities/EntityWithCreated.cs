using Patika.Shared.Interfaces;
using System;

namespace Patika.Shared.Entities
{
    public abstract class EntityWithCreated<T> : IEntity<T>, IHasCreated
    {
        public DateTime Created { get ; set; }
        public T Id { get; set; }
    

        public EntityWithCreated()
        {
            Created = DateTime.Now;
        }
    }

    public abstract class EntityWithCreated : EntityWithCreated<Guid> { }
}
using Patika.Shared.Interfaces;
using System;

namespace Patika.Shared.Entities
{
    public abstract class EntityWithCreatedAndUpdated<T> : IEntity<T>, IHasCreated, IHasUpdated
    {
        public virtual T Id { get; set; }
        public DateTime Created { get ; set; }
        public DateTime Updated { get;set; }

        public EntityWithCreatedAndUpdated()
        {
            Created = DateTime.Now;
            Updated = DateTime.Now;
        }
    }

    public abstract class EntityWithCreatedAndUpdated : EntityWithCreatedAndUpdated<Guid> { }
}
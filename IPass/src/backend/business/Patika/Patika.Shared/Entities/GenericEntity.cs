using Patika.Shared.Interfaces;
using System;

namespace Patika.Shared.Entities
{
    public abstract class GenericEntity<T> : IEntity<T>, IEquatable<T>, IHasCreated, IHasUpdated, ILogicalDelete, IActive
    {
        public virtual T Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool Deleted { get; set; }
        public bool Active { get; set; }

        public bool Equals(T other)
        {
            if (other == null)
                return false;
            if (typeof(T).Equals(other.GetType()))
                return Convert.ChangeType(other, typeof(T)) == this;
            return false;
        }
    }
}

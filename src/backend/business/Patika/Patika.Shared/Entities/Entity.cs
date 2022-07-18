using Patika.Shared.Interfaces;
using System;

namespace Patika.Shared.Entities
{
    public abstract class Entity : IEntity<Guid>
    {
        public Guid Id { get; set; }
    }
}
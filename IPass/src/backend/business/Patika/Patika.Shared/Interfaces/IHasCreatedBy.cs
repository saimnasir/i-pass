using System;

namespace Patika.Shared.Interfaces
{
    public interface IHasCreatedBy
    {
        public Guid CreatedBy { get; set; }
    }
}
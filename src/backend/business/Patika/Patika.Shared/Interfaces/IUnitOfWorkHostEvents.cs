using System;

namespace Patika.Shared.Interfaces
{
    public interface IUnitOfWorkHostEvents
    {
        event EventHandler Committed;
        event EventHandler RollBacked;
    }
}
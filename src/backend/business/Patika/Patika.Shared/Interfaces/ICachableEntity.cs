using System;

namespace Patika.Shared.Interfaces
{
    public interface ICachableEntity
    {
        //DateTime? ExpireOn {  get; set; }
        string GetCacheKey() => GetType().FullName;
        TimeSpan? GetExpireTime() => new (TimeSpan.TicksPerDay * 365);
    }
}
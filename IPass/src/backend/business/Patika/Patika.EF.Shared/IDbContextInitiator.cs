using Microsoft.EntityFrameworkCore;

namespace Patika.EF.Shared
{
    public interface IDbContextInitiator<T>
    {
        void Init(DbContextOptionsBuilder options);
    }
}
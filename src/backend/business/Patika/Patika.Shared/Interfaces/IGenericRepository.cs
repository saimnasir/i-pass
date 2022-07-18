namespace Patika.Shared.Interfaces
{
    public interface IGenericRepository<T, U> : IGenericQueryRepository<T, U>, IGenericCUDRepository<T, U> where T : IEntity<U>
    {

    }
}
namespace Patika.Shared.Interfaces
{
    public interface IUnitOfWork<IDbContext>
    {
        IDbContext DbContext { get; set; }
    }
}
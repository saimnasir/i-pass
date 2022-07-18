namespace Patika.Shared.Interfaces
{
    public interface IUnitOfWorkHostInterface : IUnitOfWorkHostEvents
    {
        object DbContext { get; }
    }
}
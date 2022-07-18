namespace Patika.Shared.Interfaces
{
    public interface ILogicalDelete
    {
        bool Deleted { get; set; }
    }
    public interface IActive
    {
        bool Active { get; set; }
    }
}
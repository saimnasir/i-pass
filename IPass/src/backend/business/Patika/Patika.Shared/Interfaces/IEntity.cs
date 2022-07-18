namespace Patika.Shared.Interfaces
{
    public interface IEntity<TIDType>
    {
        TIDType Id { get; set; }      
    }
}
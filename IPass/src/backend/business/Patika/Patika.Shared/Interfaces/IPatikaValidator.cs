using System.Threading.Tasks;

namespace Patika.Shared.Interfaces
{
    public interface IPatikaValidator<T>
    {
        Task ValidateAsync(T input); 
    }
}

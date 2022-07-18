using Patika.Shared.DTO;
using System.Threading.Tasks;

namespace Patika.Shared.Services
{
    public interface IBlobService<T> where T : IBlobDTO
    {
        Task UploadAsync(T input);
        Task<bool> RemoveAsync(T input);
        Task<bool> ExistsAsync(T input);
    }
}

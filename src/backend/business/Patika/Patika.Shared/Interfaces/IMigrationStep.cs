using System.Threading.Tasks;

namespace Patika.Shared.Interfaces
{
    public interface IMigrationStep
    {
        Task EnsureMigrationAsync();
    }
}
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace IPass.Application.Contracts.AccountDomain.Validators
{
    public interface IFileValidator
    {
        Task ValidateAsync(IFormFile file);
    }
}

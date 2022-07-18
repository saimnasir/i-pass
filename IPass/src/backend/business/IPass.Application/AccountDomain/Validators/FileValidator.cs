using IPass.Application.Contracts.AccountDomain.Validators;
using Microsoft.AspNetCore.Http;
using Patika.Shared.Exceptions.AccountDomain;
using Patika.Shared.Extensions;
using System.Threading.Tasks;

namespace IPass.Application.AccountDomain.Validators
{
    public class FileValidator : IFileValidator
    {
        public async Task ValidateAsync(IFormFile file)
        {
            
            if (file == null || file.Length == 0)
                throw new FormFileRequiredException();

            const int maxFileSize = 5242880; // TODO : Confige eklenecek
            if (file.Length > maxFileSize)
                throw new FormFileTooLargeException();

            if (file.GetExtension().GetImageFormat() == ImageMagick.MagickFormat.Unknown)
                throw new FormFileTypeIncorrectException();


            await Task.FromResult(0);
        }
    }

}

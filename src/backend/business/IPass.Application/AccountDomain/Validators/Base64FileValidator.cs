using IPass.Application.Contracts.AccountDomain.Validators;
using Microsoft.AspNetCore.Http;
using Patika.Shared.DTO.Validators;
using Patika.Shared.Extensions;
using System.Threading.Tasks;

namespace IPass.Application.AccountDomain.Validators
{
    public class Base64FileValidator : IBase64FileValidator
    {
        public Base64FileValidator(IFileValidator formFileValidator)
        {
            FormFileValidator = formFileValidator;
        }
        IFileValidator FormFileValidator { get; }
        public async Task ValidateAsync(FileValidatorInputDto input)
        {
            IFormFile file = input.File.ConvertBase64ToFormFile(input.FileNameWithExtension);
            await FormFileValidator.ValidateAsync(file);
        }
    }
}

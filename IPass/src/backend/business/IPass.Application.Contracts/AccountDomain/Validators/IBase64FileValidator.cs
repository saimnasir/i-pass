using Patika.Shared.DTO.Validators;
using Patika.Shared.Interfaces;

namespace IPass.Application.Contracts.AccountDomain.Validators
{
    public interface IBase64FileValidator : IPatikaValidator<FileValidatorInputDto>
    {
    }
}

using IPass.Shared.DTO.CommonDomain;
using IPass.Shared.DTO.PasswordDomain;
using Patika.Application.Contracts;
using Patika.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace IPass.Application.Contracts.PasswordDomain
{
    public interface IProfileAppService : IApplicationService
    {
        Task<SingleResponse<PinCodeDto>> CheckPinCodeAsync(CheckPinCodeInputDto input);
        Task<SingleResponse<PinCodeDto>> CreatePinCodeAsync(CreatePinCodeInputDto input);
        Task<SingleResponse<ProfileDto>> GetProfileAsync(DTO input);
        Task  UpdateProfileAsync(UserDto input);
    }
}

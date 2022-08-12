using IPass.Application.Contracts.PasswordDomain;
using IPass.Domain.CommonDomain.Entities;
using IPass.Domain.CommonDomain.Repositories;
using IPass.Domain.PasswordDomain.Entities;
using IPass.Domain.PasswordDomain.Repositories;
using IPass.Shared.DTO.CommonDomain;
using IPass.Shared.DTO.PasswordDomain;
using IPass.Shared.Services;
using Patika.Application;
using Patika.Shared.DTO;
using Patika.Shared.Entities;
using Patika.Shared.Enums;
using Patika.Shared.Extensions;
using Patika.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace IPass.Application.PasswordDomain
{
    public class ProfileAppService : ApplicationService, IProfileAppService
    {
        IUserRepository UserRepository { get; }
        IPinCodeRepository PinCodeRepository { get; }
        IUnitOfWorkHostWithInterface UnitOfWork { get; }

        public ProfileAppService(
            IUserRepository userRepository,
            IPinCodeRepository passwordRepository,
            IUnitOfWorkHostWithInterface unitOfWorkHostWithInterface,
            Configuration configuration,
            ILogWriter jobLogger
        ) : base(configuration, new GeneralMappingProfile(), jobLogger)
        {
            UserRepository = userRepository;
            PinCodeRepository = passwordRepository;
            UnitOfWork = unitOfWorkHostWithInterface;
        }

        public async Task<SingleResponse<PinCodeDto>> CheckPinCodeAsync(CheckPinCodeInputDto input)
        {
            return await WithLogging(input, GetType(), async () =>
            {
                var pinCode = await PinCodeRepository.FirstOrDefaultAsync(p => p.Active && p.Code == input.Code && p.UserId == ApplicationUser.GetGuid());
                if (pinCode == null)
                {
                    throw new Exception($"Pin Code bulunamadı.");
                }
                var pinCodeDto = Mapper.Map<PinCodeDto>(pinCode);
                //if (pinCodeDto.Expired)
                //{
                //    throw new Exception($"Pin Code expired.");
                //}
                return new SingleResponse<PinCodeDto>()
                {
                    Data = pinCodeDto
                };
            });
        }

        public async Task<SingleResponse<PinCodeDto>> CreatePinCodeAsync(CreatePinCodeInputDto input)
        {
            return await WithLogging(input, GetType(), async () =>
            {
                await UnitOfWork.BeginTransactionAsync();
                try
                {
                    // CAN : Can be at profile settings (PinCode Expiration:mins)
                    var seconds = (long)DateTime.Now.AddMinutes(1).Subtract(DateTime.Now).TotalSeconds;

                    var pinCode = new PinCode
                    {
                        Code = input.Code,
                        UserId = ApplicationUser.GetGuid(),
                        Expiration = seconds,
                        Active = true,
                    };

                    var olds = await PinCodeRepository.WhereAsync(s => s.Active && s.UserId == ApplicationUser.GetGuid());
                    olds = olds.Select(s => { s.Active = false; return s; });
                    await PinCodeRepository.UpdateManyAsync(olds, UnitOfWork);
                    await PinCodeRepository.InsertOneAsync(pinCode, UnitOfWork);

                    await UnitOfWork.CommitAsync();
                    var pinCodeDto = Mapper.Map<PinCodeDto>(pinCode);
                    return new SingleResponse<PinCodeDto>()
                    {
                        Data = pinCodeDto
                    };
                }
                catch (Exception ex)
                {
                    await UnitOfWork.RollbackAsync();
                    throw;
                }
            });
        }

        public async Task<SingleResponse<ProfileDto>> GetProfileAsync(DTO input)
        {
            return await WithLogging(input, GetType(), async () =>
            {
                var user = await UserRepository.GetByIdAsync(ApplicationUser.GetGuid());
                var pinCode = await PinCodeRepository.FirstOrDefaultAsync(p => p.Active && p.UserId == ApplicationUser.GetGuid());
                var profileDto = new ProfileDto
                {
                    PinCode = Mapper.Map<PinCodeDto>(pinCode),
                    User = Mapper.Map<UserDto>(user),
                };
                profileDto.User.UserName = ApplicationUser.UserName;
                profileDto.User.Email = ApplicationUser.Email;
                return new SingleResponse<ProfileDto>()
                {
                    Data = profileDto
                };
            });
        }

        public async Task  UpdateProfileAsync(UserDto input)
        {
            await WithLogging(input, GetType(), async () =>
            {
                var user = Mapper.Map<User>(input);
                await UserRepository.UpdateOneAsync(user); 
            });
        }
    }
}

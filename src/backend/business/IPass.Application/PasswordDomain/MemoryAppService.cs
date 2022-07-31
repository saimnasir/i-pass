using IPass.Application.Contracts.PasswordDomain;
using IPass.Domain.PasswordDomain.Entities;
using IPass.Domain.PasswordDomain.Repositories;
using IPass.Shared.DTO.PasswordDomain;
using IPass.Shared.Services;
using Patika.Application;
using Patika.Shared.DTO;
using Patika.Shared.Entities;
using Patika.Shared.Enums;
using Patika.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace IPass.Application.PasswordDomain
{
    public class MemoryAppService : ApplicationService, IMemoryAppService
    {
        IMemoryRepository MemoryRepository { get; }
        IEnvironmentTypeAppService EnvironmentTypeAppService { get; }
        IOrganizationAppService OrganizationAppService { get; }
        IMemoryTypeAppService MemoryTypeAppService { get; }

        public MemoryAppService(
            IMemoryRepository organizationRepository,
            IEnvironmentTypeAppService environmentTypeAppService,
            IOrganizationAppService organizationAppService,
            IMemoryTypeAppService organizationTypeAppService,
            Configuration configuration,
            ILogWriter jobLogger
        ) : base(configuration, new GeneralMappingProfile(), jobLogger)
        {
            MemoryRepository = organizationRepository;
            EnvironmentTypeAppService = environmentTypeAppService;
            OrganizationAppService = organizationAppService;
            MemoryTypeAppService = organizationTypeAppService;
        }

        public async Task<object> AddAsync(CreateMemoryInputDto input)
        {
            return await WithLogging<object>(input, GetType(), async () =>
            {
                // check organization existance
                var organization = (await OrganizationAppService.GetAsync(new IdInputDto<Guid>(input.OrganizationId) { LogId = input.LogId })).Data;

                // check memory type existance
                var memoryType = (await MemoryTypeAppService.GetAsync(new IdInputDto<Guid>(input.MemoryTypeId) { LogId = input.LogId })).Data;

                if (string.IsNullOrEmpty(input.Title))
                {
                    input.Title = $"{organization.Title}-{memoryType.Title}";
                }
                // check existance by name
                var isExists = await MemoryRepository.AnyAsync(s => s.Title == input.Title);
                if (isExists)
                {
                    throw new Exception($"{input.Title} hafıza kaydı zaten eklenmiş.");
                }
                // check organization type existance
                if (input.EnvironmentTypeId.HasValue && input.EnvironmentTypeId.Value != Guid.Empty)
                {
                    await EnvironmentTypeAppService.GetAsync(new IdInputDto<Guid>(input.EnvironmentTypeId.Value) { LogId = input.LogId });
                }

                if (string.IsNullOrEmpty(input.UserName))
                {
                    input.UserName = input.Email;
                }

                var memory = Mapper.Map<Memory>(input);
                EncodeMemory(memory);
                await MemoryRepository.InsertOneAsync(memory);
                return true;
            });
        }

        private void EncodeMemory(Memory memory)
        {
            memory.Password = memory.Password.Encode();
            if (memory.IsUserNameSecure)
            {
                memory.UserName = memory.UserName.Encode();
            }
            if (memory.IsUEmailSecure)
            {
                memory.Email = memory.Email.Encode();
            }
            if (memory.IsHostOrIpAddressSecure)
            {
                memory.HostOrIpAddress = memory.HostOrIpAddress.Encode();
            }
            if (memory.IsPortSecure)
            {
                memory.Port = memory.Port.Encode();
            }
        }

        private void DecodeMemory(Memory memory)
        {
            memory.Password = memory.Password.Decode();
            if (memory.IsUserNameSecure)
            {
                memory.UserName = memory.UserName.Decode();
            }
            if (memory.IsUEmailSecure)
            {
                memory.Email = memory.Email.Decode();
            }
            if (memory.IsHostOrIpAddressSecure)
            {
                memory.HostOrIpAddress = memory.HostOrIpAddress.Decode();
            }
            if (memory.IsPortSecure)
            {
                memory.Port = memory.Port.Decode();
            }
        }

        public async Task<object> UpdateAsync(MemoryDto input)
        {
            return await WithLogging<object>(input, GetType(), async () =>
            {
                var currentMemory = await MemoryRepository.GetByIdAsync(input.Id);
                if (currentMemory != null)
                {
                    var nextMemory = Mapper.Map<Memory>(input);
                    EncodeMemory(nextMemory);
                    await MemoryRepository.UpdateOneAsync(nextMemory);
                }
                else
                {
                    throw new Exception("Memory not exist");
                }
                return true;
            });
        }

        public async Task<ListResponse<MemoryDto>> GetListAsync(SearchMemoriesInputDto input)
        {
            return await WithLogging(input, GetType(), async () =>
            {
                var conditions = new List<Condition>();
                var sorts = new List<Sort>();
                if (!string.IsNullOrEmpty(input.SortBy) && input.SortType != SortType.None)
                {
                    sorts.Add(new Sort { Column = input.SortBy, SortType = input.SortType });
                }
                //if (!string.IsNullOrEmpty(input.SearchText))
                //{
                //    conditions.Add(new Condition
                //    {
                //        Operator = ConditionOperator.Contains,
                //        PropertyName = nameof(Memory.Title),
                //        Values = new List<string> { input.SearchText.ToString() }
                //    });
                //}

                var pagination = new Pagination(input.Page, input.PageSize);
                var query = await MemoryRepository.WhereAsync(conditions, sorts: sorts, includeAll: true);

                if (!string.IsNullOrEmpty(input.SearchText))
                {
                    query = query.Where(m =>
                    m.Title.Contains(input.SearchText, StringComparison.CurrentCultureIgnoreCase)
                    || m.Organization.Title.Contains(input.SearchText, StringComparison.CurrentCultureIgnoreCase)
                    || m.Organization.OrganizationType.Title.Contains(input.SearchText, StringComparison.CurrentCultureIgnoreCase)
                    || m.MemoryType.Title.Contains(input.SearchText, StringComparison.CurrentCultureIgnoreCase)
                    //|| (m.EnvironmentType != null && m.EnvironmentType.Title.Contains(input.SearchText, StringComparison.CurrentCultureIgnoreCase))
                    );
                }

                var pagedResult = query.AsQueryable().Sort(sorts).Paginate(pagination);

                var memories = pagedResult.Queryable.ToList();

                if (input.Decode)
                {
                    memories.Select(memory => { DecodeMemory(memory); return memory; }).ToList();
                }

                var passwordDtos = Mapper.Map<List<MemoryDto>>(memories);

                return new ListResponse<MemoryDto>()
                {
                    Page = pagedResult.CurrentPage,
                    PageSize = pagedResult.PageSize,
                    TotalCount = pagedResult.RowCount,
                    PageCount = pagedResult.PageCount,
                    Data = passwordDtos
                };

            });
        }

        public async Task<ListResponse<MemoryDto>> GetHistoriesAsync(SearchMemoryHisyoriesInputDto input)
        {
            return await WithLogging(input, GetType(), async () =>
            {
                var conditions = new List<Condition>();

                var pagination = new Pagination
                {
                    Page = input.Page < 1 ? 1 : input.Page,
                    Count = input.PageSize < 0 ? 20 : input.PageSize
                };
                var query = await MemoryRepository.GetHistoriesAsync(input.Id);
                var pagedResult = query.AsQueryable().Paginate(pagination);

                var memories = pagedResult.Queryable.ToList();

                if (input.Decode)
                {
                    memories.Select(memory => { DecodeMemory(memory); return memory; }).ToList();
                }

                var passwordDtos = Mapper.Map<List<MemoryDto>>(memories);

                return new ListResponse<MemoryDto>()
                {
                    Page = pagedResult.CurrentPage,
                    PageSize = pagedResult.PageSize,
                    TotalCount = pagedResult.RowCount,
                    PageCount = pagedResult.PageCount,
                    Data = passwordDtos
                };

            });
        }

        public async Task<SingleResponse<MemoryDto>> GetAsync(GetMemoryByIdInputDto input)
        {
            return await WithLogging(input, GetType(), async () =>
            {
                var memory = await MemoryRepository.GetByIdAsync(input.Id);
                if (memory == null)
                {
                    throw new Exception($"{input.Id} kayıt bulunamadı.");
                }

                if (input.Decode)
                {
                    DecodeMemory(memory);
                }
                return new SingleResponse<MemoryDto>()
                {
                    Data = Mapper.Map<MemoryDto>(memory)
                };

            });
        }

    }
}

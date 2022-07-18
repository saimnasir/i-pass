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
    public class MemoryTypeAppService : ApplicationService, IMemoryTypeAppService
    {
        IMemoryTypeRepository MemoryTypeRepository { get; }

        public MemoryTypeAppService(
            IMemoryTypeRepository passwordRepository,
            Configuration configuration,
            ILogWriter jobLogger
        ) : base(configuration, new GeneralMappingProfile(), jobLogger)
        {
            MemoryTypeRepository = passwordRepository;
        }

        public async Task<object> AddAsync(CreateMemoryTypeInputDto input)
        {
            return await WithLogging<object>(input, GetType(), async () =>
            {
                var isExists = await MemoryTypeRepository.AnyAsync(s => string.Equals(s.Title.ToLower(), input.Title.ToLower()));
                if (isExists)
                {
                    throw new Exception($"{input.Title} hafıza tipi zaten eklenmiş.");
                }
                
                var password = Mapper.Map<MemoryType>(input);
                await MemoryTypeRepository.InsertOneAsync(password);
                return true;
            });
        }

        public async Task<object> UpdateAsync(MemoryTypeDto input)
        {
            return await WithLogging<object>(input, GetType(), async () =>
            {
                var currentMemoryType = await MemoryTypeRepository.GetByIdAsync(input.Id);
                if (currentMemoryType != null)
                {
                    var nextMemoryType = Mapper.Map<MemoryType>(input);
                    await MemoryTypeRepository.UpdateOneAsync(nextMemoryType);
                }
                else
                {
                    throw new Exception("MemoryType not exist");
                }
                return true;
            });
        }

        public async Task<ListResponse<MemoryTypeDto>> GetListAsync(SearchInputDto input)
        {
            return await WithLogging(input, GetType(), async () =>
            {
                var conditions = new List<Condition>();

                if (!string.IsNullOrEmpty(input.SearchText))
                {
                    conditions.Add(new Condition
                    {
                        Operator = ConditionOperator.Contains,
                        PropertyName = nameof(MemoryType.Title),
                        Values = new List<string> { input.SearchText.ToString() }
                    });
                }

                var pagination = new Pagination
                {
                    Page = input.Page < 1 ? 1 : input.Page,
                    Count = input.PageSize < 0 ? 20 : input.PageSize
                };
                var query = await MemoryTypeRepository.WhereAsync(conditions, includeAll: true);
                var pagedResult = query.AsQueryable().Paginate(pagination);

                var passwordDtos = Mapper.Map<List<MemoryTypeDto>>(pagedResult.Queryable.ToList());

                return new ListResponse<MemoryTypeDto>()
                {
                    Page = pagedResult.CurrentPage,
                    PageSize = pagedResult.PageSize,
                    TotalCount = pagedResult.RowCount,
                    PageCount = pagedResult.PageCount,
                    Data = passwordDtos
                };

            });
        }

        public async Task<SingleResponse<MemoryTypeDto>> GetAsync(IdInputDto<Guid> input)
        {
            return await WithLogging(input, GetType(), async () =>
            {
                var organizationType = await MemoryTypeRepository.GetByIdAsync(input.Id);
                if (organizationType == null)
                {
                    throw new Exception($"{input.Id} organizasyon tipi bulunamadı.");
                }
                return new SingleResponse<MemoryTypeDto>()
                {
                    Data = Mapper.Map<MemoryTypeDto>(organizationType)
                };

            });
        }
    }
}

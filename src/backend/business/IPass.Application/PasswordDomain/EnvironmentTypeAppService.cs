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
    public class EnvironmentTypeAppService : ApplicationService, IEnvironmentTypeAppService
    {
        IEnvironmentTypeRepository EnvironmentTypeRepository { get; }

        public EnvironmentTypeAppService(
            IEnvironmentTypeRepository passwordRepository,
            Configuration configuration,
            ILogWriter jobLogger
        ) : base(configuration, new GeneralMappingProfile(), jobLogger)
        {
            EnvironmentTypeRepository = passwordRepository;
        }

        public async Task<object> AddAsync(CreateEnvironmentTypeInputDto input)
        {
            return await WithLogging<object>(input, GetType(), async () =>
            {
                var isExists = await EnvironmentTypeRepository.AnyAsync(s => string.Equals(s.Title.ToLower(), input.Title.ToLower()));
                if (isExists)
                {
                    throw new Exception($"{input.Title} ortam tipi zaten eklenmiş.");
                }
                var password = Mapper.Map<EnvironmentType>(input);
                await EnvironmentTypeRepository.InsertOneAsync(password);
                return true;
            });
        }
        public async Task<object> UpdateAsync(EnvironmentTypeDto input)
        {
            return await WithLogging<object>(input, GetType(), async () =>
            {
                var currentEnvironmentType = await GetAsync(new IdInputDto<Guid>(input.Id) { LogId = input.LogId });
                if (currentEnvironmentType.Data != null)
                {
                    var nextEnvironmentType = Mapper.Map<EnvironmentType>(input);
                    await EnvironmentTypeRepository.UpdateOneAsync(nextEnvironmentType);
                }
                else
                {
                    throw new Exception("EnvironmentType not exist");
                }
                return true;
            });
        }

        public async Task<ListResponse<EnvironmentTypeDto>> GetListAsync(SearchInputDto input)
        {
            return await WithLogging(input, GetType(), async () =>
            {
                var conditions = new List<Condition>();

                if (!string.IsNullOrEmpty(input.SearchText))
                {
                    conditions.Add(new Condition
                    {
                        Operator = ConditionOperator.Contains,
                        PropertyName = nameof(EnvironmentType.Title),
                        Values = new List<string> { input.SearchText.ToString() }
                    });
                }

                var pagination = new Pagination
                {
                    Page = input.Page < 1 ? 1 : input.Page,
                    Count = input.PageSize < 0 ? 20 : input.PageSize
                };
                var query = await EnvironmentTypeRepository.WhereAsync(conditions);
                var pagedResult = query.AsQueryable().Paginate(pagination);

                var passwordDtos = Mapper.Map<List<EnvironmentTypeDto>>(pagedResult.Queryable.ToList());

                return new ListResponse<EnvironmentTypeDto>()
                {
                    Page = pagedResult.CurrentPage,
                    PageSize = pagedResult.PageSize,
                    TotalCount = pagedResult.RowCount,
                    PageCount = pagedResult.PageCount,
                    Data = passwordDtos
                };

            });
        }

        public async Task<SingleResponse<EnvironmentTypeDto>> GetAsync(IdInputDto<Guid> input)
        {
            return await WithLogging(input, GetType(), async () =>
            {
                var organizationType = await EnvironmentTypeRepository.GetByIdAsync(input.Id);
                if (organizationType == null)
                {
                    throw new Exception($"{input.Id} ortam tipi bulunamadı.");
                }
                return new SingleResponse<EnvironmentTypeDto>()
                {
                    Data = Mapper.Map<EnvironmentTypeDto>(organizationType)
                };

            });
        }
    }
}

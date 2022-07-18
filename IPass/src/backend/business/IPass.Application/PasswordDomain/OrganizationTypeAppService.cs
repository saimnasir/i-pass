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
    public class OrganizationTypeAppService : ApplicationService, IOrganizationTypeAppService
    {
        IOrganizationTypeRepository OrganizationTypeRepository { get; }

        public OrganizationTypeAppService(
            IOrganizationTypeRepository passwordRepository,
            Configuration configuration,
            ILogWriter jobLogger
        ) : base(configuration, new GeneralMappingProfile(), jobLogger)
        {
            OrganizationTypeRepository = passwordRepository;
        }

        public async Task<object> AddAsync(CreateOrganizationTypeInputDto input)
        {
            return await WithLogging<object>(input, GetType(), async () =>
            {
                var isExists = await OrganizationTypeRepository.AnyAsync(s => string.Equals(s.Title.ToLower(), input.Title.ToLower()));
                if (isExists)
                {
                    throw new Exception($"{input.Title} organizasyon tipi zaten eklenmiş.");
                }
                var password = Mapper.Map<OrganizationType>(input);
                await OrganizationTypeRepository.InsertOneAsync(password);
                return true;
            });
        }
        public async Task<object> UpdateAsync(OrganizationTypeDto input)
        {
            return await WithLogging<object>(input, GetType(), async () =>
            {
                var currentOrganizationType = await OrganizationTypeRepository.GetByIdAsync(input.Id);
                if (currentOrganizationType != null)
                {
                    var nextOrganizationType = Mapper.Map<OrganizationType>(input);
                    await OrganizationTypeRepository.UpdateOneAsync(nextOrganizationType);
                }
                else
                {
                    throw new Exception("OrganizationType not exist");
                }
                return true;
            });
        }

        public async Task<ListResponse<OrganizationTypeDto>> GetListAsync(SearchInputDto input)
        {
            return await WithLogging(input, GetType(), async () =>
            {
                var conditions = new List<Condition>();

                if (!string.IsNullOrEmpty(input.SearchText))
                {
                    conditions.Add(new Condition
                    {
                        Operator = ConditionOperator.Contains,
                        PropertyName = nameof(OrganizationType.Title),
                        Values = new List<string> { input.SearchText.ToString() }
                    });
                }

                var pagination = new Pagination
                {
                    Page = input.Page < 1 ? 1 : input.Page,
                    Count = input.PageSize < 0 ? 20 : input.PageSize
                };
                var query = await OrganizationTypeRepository.WhereAsync(conditions);
                var pagedResult = query.AsQueryable().Paginate(pagination);

                var passwordDtos = Mapper.Map<List<OrganizationTypeDto>>(pagedResult.Queryable.ToList());

                return new ListResponse<OrganizationTypeDto>()
                {
                    Page = pagedResult.CurrentPage,
                    PageSize = pagedResult.PageSize,
                    TotalCount = pagedResult.RowCount,
                    PageCount = pagedResult.PageCount,
                    Data = passwordDtos
                };

            });
        }

        public async Task<SingleResponse<OrganizationTypeDto>> GetAsync(IdInputDto<Guid> input)
        {
            return await WithLogging(input, GetType(), async () =>
            {
                var organizationType = await OrganizationTypeRepository.GetByIdAsync(input.Id);
                if (organizationType == null)
                {
                    throw new Exception($"{input.Id} organizasyon tipi bulunamadı.");
                }
                return new SingleResponse<OrganizationTypeDto>()
                {
                    Data = Mapper.Map<OrganizationTypeDto>(organizationType)
                };

            });
        }
    }
}

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
    public class OrganizationAppService : ApplicationService, IOrganizationAppService
    {
        IOrganizationRepository OrganizationRepository { get; }
        IOrganizationTypeAppService OrganizationTypeAppService { get; }

        public OrganizationAppService(
            IOrganizationRepository organizationRepository,
            IOrganizationTypeAppService organizationTypeAppService,
            Configuration configuration,
            ILogWriter jobLogger
        ) : base(configuration, new GeneralMappingProfile(), jobLogger)
        {
            OrganizationRepository = organizationRepository;
            OrganizationTypeAppService = organizationTypeAppService;
        }

        public async Task<object> AddAsync(CreateOrganizationInputDto input)
        {
            return await WithLogging<object>(input, GetType(), async () =>
            {
                // check organization type existance
               var organizationType =  (await OrganizationTypeAppService.GetAsync(new IdInputDto<Guid>(input.OrganizationTypeId) { LogId = input.LogId })).Data;
                
                // check existance by name
                var isExists = await OrganizationRepository.AnyAsync(s => organizationType.Id == s.OrganizationTypeId && string.Equals(s.Title.ToLower(), input.Title.ToLower()));
                if (isExists)
                {
                    throw new Exception($"{input.Title} organizasyon, {organizationType.Title} tipinde zaten eklenmiş.");
                } 
                // check parent organization type existance if set
                if (input.ParentOrganizationId.HasValue && input.ParentOrganizationId.Value != Guid.Empty)
                {
                    await GetParentAsync(new IdInputDto<Guid>(input.ParentOrganizationId.Value) { LogId = input.LogId });
                } 
               
                var organization = Mapper.Map<Organization>(input);
                await OrganizationRepository.InsertOneAsync(organization);
                return true;
            });
        }
        public async Task<object> UpdateAsync(OrganizationDto input)
        {
            return await WithLogging<object>(input, GetType(), async () =>
            {
                var currentOrganization = await OrganizationRepository.GetByIdAsync(input.Id);
                if (currentOrganization != null)
                {
                    var nextOrganization = Mapper.Map<Organization>(input);
                    await OrganizationRepository.UpdateOneAsync(nextOrganization);
                }
                else
                {
                    throw new Exception("Organization not exist");
                }
                return true;
            });
        }

        public async Task<ListResponse<OrganizationDto>> GetListAsync(SearchInputDto input)
        {
            return await WithLogging(input, GetType(), async () =>
            {
                var conditions = new List<Condition>();

                if (!string.IsNullOrEmpty(input.SearchText))
                {
                    conditions.Add(new Condition
                    {
                        Operator = ConditionOperator.Contains,
                        PropertyName = nameof(Organization.Title),
                        Values = new List<string> { input.SearchText.ToString() }
                    });
                }

                var pagination = new Pagination
                {
                    Page = input.Page < 1 ? 1 : input.Page,
                    Count = input.PageSize < 0 ? 20 : input.PageSize
                };
                var query = await OrganizationRepository.WhereAsync(conditions, includeAll:true);
                var pagedResult = query.AsQueryable().Paginate(pagination);

                var passwordDtos = Mapper.Map<List<OrganizationDto>>(pagedResult.Queryable.ToList());

                return new ListResponse<OrganizationDto>()
                {
                    Page = pagedResult.CurrentPage,
                    PageSize = pagedResult.PageSize,
                    TotalCount = pagedResult.RowCount,
                    PageCount = pagedResult.PageCount,
                    Data = passwordDtos
                };

            });
        }

        public async Task<SingleResponse<OrganizationDto>> GetAsync(IdInputDto<Guid> input)
        {
            return await WithLogging(input, GetType(), async () =>
            {
                var password = await OrganizationRepository.GetByIdAsync(input.Id);
                if (password == null)
                {
                    throw new Exception($"{input.Id} organizasyon bulunamadı.");
                }
                return new SingleResponse<OrganizationDto>()
                {
                    Data = Mapper.Map<OrganizationDto>(password)
                };

            });
        }

        private async Task<SingleResponse<OrganizationDto>> GetParentAsync(IdInputDto<Guid> input)
        {
            return await WithLogging(input, GetType(), async () =>
            {
                var organization = await OrganizationRepository.GetByIdAsync(input.Id);
                if (organization == null)
                {
                    throw new Exception($"{input.Id} üst organizasyon bulunamadı.");
                }
                return new SingleResponse<OrganizationDto>()
                {
                    Data = Mapper.Map<OrganizationDto>(organization)
                };
            });
        }
    }
}

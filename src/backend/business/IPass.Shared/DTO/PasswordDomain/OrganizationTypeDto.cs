using System;

namespace IPass.Shared.DTO.PasswordDomain
{
    public class OrganizationTypeDto : GenericDto<Guid>
    {
        public string Title { get; set; }
    }
}

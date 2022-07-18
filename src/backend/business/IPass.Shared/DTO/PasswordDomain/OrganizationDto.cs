using System;

namespace IPass.Shared.DTO.PasswordDomain
{
    public class OrganizationDto : GenericDto<Guid>
    {
        public string Title { get; set; }
        public Guid OrganizationTypeId { get; set; }
        public OrganizationTypeDto OrganizationType { get; set; }

        public Guid? ParentOrganizationId { get; set; }
        public OrganizationDto ParentOrganization { get; set; }
    }
}

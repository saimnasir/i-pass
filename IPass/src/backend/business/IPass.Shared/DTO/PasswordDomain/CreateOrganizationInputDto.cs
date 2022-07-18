using System;

namespace IPass.Shared.DTO.PasswordDomain
{
    public class CreateOrganizationInputDto : Patika.Shared.DTO.DTO
    {
        public string Title { get; set; }
        public Guid OrganizationTypeId { get; set; } 
        public Guid? ParentOrganizationId { get; set; }
    }
}

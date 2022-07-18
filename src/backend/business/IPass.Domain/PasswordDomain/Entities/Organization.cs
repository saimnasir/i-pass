using Patika.Shared.Entities;
using System;

namespace IPass.Domain.PasswordDomain.Entities
{
    public class Organization : GenericEntity<Guid>
    {
        public string Title { get; set; }
        public Guid OrganizationTypeId { get; set; }
        public virtual OrganizationType OrganizationType { get; set; }

        public Guid? ParentOrganizationId { get; set; }
        public virtual Organization  ParentOrganization { get; set; }
    }
}

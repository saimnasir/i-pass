using Patika.Shared.Entities;
using Patika.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPass.Domain.PasswordDomain.Entities
{ 
    public class EnvironmentType : GenericEntity<Guid>
    {
        public string Title { get; set; } 
    }
}

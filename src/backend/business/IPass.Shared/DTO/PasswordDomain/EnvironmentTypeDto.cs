using System;

namespace IPass.Shared.DTO.PasswordDomain
{
    public class EnvironmentTypeDto : GenericDto<Guid>
    {
        public string Title { get; set; }
    }
}

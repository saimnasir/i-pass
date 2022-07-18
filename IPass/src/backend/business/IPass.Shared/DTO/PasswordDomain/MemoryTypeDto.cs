using System;

namespace IPass.Shared.DTO.PasswordDomain
{
    public class MemoryTypeDto : GenericDto<Guid>
    {
        public string Title { get; set; }
    }
}

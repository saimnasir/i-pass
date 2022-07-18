using System;

namespace IPass.Shared.DTO.PasswordDomain
{
    public class CreatePasswordInputDto : Patika.Shared.DTO.DTO
    {
        public string Title { get; set; }
        public string PasswordHash { get; set; } 
    }
}

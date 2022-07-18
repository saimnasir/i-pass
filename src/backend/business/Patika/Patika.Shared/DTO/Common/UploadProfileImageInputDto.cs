using Microsoft.AspNetCore.Http;

namespace IPass.Shared.DTO.Common
{
    public class UploadProfileImageInputDto : Patika.Shared.DTO.DTO
    {
        public IFormFile Photo { get; set; }
    }
}

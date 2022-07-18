using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace IPass.Shared.DTO.Common
{
    public class UploadTempPhotoInputDto : Patika.Shared.DTO.DTO
    {
        public ICollection<IFormFile> Photos { get; set; }
    }
}

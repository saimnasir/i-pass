using System.Collections.Generic;

namespace IPass.Shared.DTO.Common
{
    public class UploadTempPhotoOutputResponse : Patika.Shared.DTO.DTO
    {
        public ICollection<string> Photos { get; set; }
    }
}

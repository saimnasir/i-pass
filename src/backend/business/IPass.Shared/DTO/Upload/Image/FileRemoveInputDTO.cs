using Patika.Shared.Enums;

namespace IPass.Shared.DTO.Upload.Image
{
    public class FileRemoveInputDTO: Patika.Shared.DTO.DTO
    {  
        public FileTypes FileType { get; set; }
        public string PhotoId { get; set; }
    }
}

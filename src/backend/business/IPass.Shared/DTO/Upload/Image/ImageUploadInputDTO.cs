using Patika.Shared.Enums;

namespace IPass.Shared.DTO.Upload.Image
{
    public class ImageUploadInputDTO<T> : Patika.Shared.DTO.DTO
    {
        public T File { get; set; } 
        public string FileName { get; set; }
        public string Extension { get; set; }
        public FileTypes FileType { get; set; }
        public string PhotoId { get; set; }
    }
}

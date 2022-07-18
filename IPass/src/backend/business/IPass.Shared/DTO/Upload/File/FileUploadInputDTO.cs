using Patika.Shared.Enums;

namespace IPass.Shared.DTO.Upload.File
{
    public class FileUploadInputDTO<T> : Patika.Shared.DTO.DTO
    {
        public T FileContent { get; set; }
        public string FileName { get; set; }  
        public string Extension { get; set; }
        public FileTypes FileType { get; } = FileTypes.Files; 
    }
}

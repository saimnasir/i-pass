using Patika.Shared.DTO;

namespace IPass.Shared.DTO.Upload.File
{
    public class FileUploadResponseDTO : GeneralResponseDTO<FileUploadResponseDTO>
    {        
        public string FileName { get; set; }
    }
}

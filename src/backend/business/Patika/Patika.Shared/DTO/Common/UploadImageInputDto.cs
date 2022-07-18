namespace IPass.Shared.DTO.Common
{
    public class UploadImageInputDto<T> : Patika.Shared.DTO.DTO
    {
        public T File { get; set; }
    }
}

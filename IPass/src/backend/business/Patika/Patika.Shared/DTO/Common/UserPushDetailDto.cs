namespace IPass.Shared.DTO.Common
{
    public class UserPushDetailDto : Patika.Shared.DTO.DTO
    { 
        public int DeviceTypeId { get; set; }
        public string PushToken { get; set; } 
    }
}

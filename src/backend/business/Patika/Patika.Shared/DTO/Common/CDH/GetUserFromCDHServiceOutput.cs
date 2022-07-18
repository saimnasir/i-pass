namespace IPass.Shared.DTO.Common.CDH
{
    public class GetUserFromCDHServiceOutput
    {
        public bool IsSuccessfull { get; set; }
        public bool Count { get; set; }
        public GetUserFromCDHServiceDataOutput Data { get; set; }
    }
}

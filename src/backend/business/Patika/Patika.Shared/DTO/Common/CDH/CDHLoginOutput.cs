namespace IPass.Shared.DTO.Common.CDH
{
    public class CDHLoginOutput
    {
        public bool IsSuccessfull { get; set; }
        public int Count { get; set; }
        public CDHLoginTokenOutput Data { get; set; }
    }
}

using System;

namespace IPass.Shared.DTO.Common.CDH
{
    public class CDHLoginTokenOutput
    {
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }
}

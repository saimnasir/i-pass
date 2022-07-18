using System.Collections.Generic;

namespace IPass.Shared.DTO.Common.CDH
{
    public class GetUserFromCDHServiceDataOutput
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string ServiceId { get; set; }
        public string MobilePhone { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string BirthDate { get; set; }
        public string CustomerTypeId { get; set; }
        public string IsActive { get; set; }
        public string StoreId { get; set; }
        public string MobilePhoneType { get; set; }
        public string MobilePhoneVerified { get; set; }
        public string MobilePhoneActive { get; set; }
        public string Phone { get; set; }
        public string PhoneType { get; set; }
        public string PhoneVerified { get; set; }
        public string Email { get; set; }
        public string EmailVerified { get; set; }
        public string SysCreateDate { get; set; }
        public string SysModifyDate { get; set; }
        public string IdentityNumber { get; set; }
        public string VknNumber { get; set; }
        public IDictionary<string, string> Attributes { get; set; }
    }
}

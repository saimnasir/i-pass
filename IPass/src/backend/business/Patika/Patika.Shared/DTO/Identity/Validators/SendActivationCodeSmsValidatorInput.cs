using System;

namespace IPass.Shared.DTO.Identity.Validators
{
    public class SendActivationCodeSmsValidatorInput
    {
        public int ActivationCodeMaxTryCount { get; set; }
        public int ActivationCodeTryCount { get; set; }
        public DateTime ActivationCodeExpireDate { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
    }
}

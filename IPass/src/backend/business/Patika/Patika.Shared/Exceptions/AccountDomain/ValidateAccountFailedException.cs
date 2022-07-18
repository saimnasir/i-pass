namespace Patika.Shared.Exceptions.AccountDomain
{
    public class ValidateAccountFailedException : ApplicationException
    {
        public ValidateAccountFailedException() : base("IDN:0039", $"Hesap onaylama hata aldı")
        {
        }
    }
}

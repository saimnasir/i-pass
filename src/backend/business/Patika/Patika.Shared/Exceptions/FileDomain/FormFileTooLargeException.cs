namespace Patika.Shared.Exceptions.AccountDomain
{
    public class FormFileTooLargeException : BaseApplicationException
    {
        public FormFileTooLargeException() : base ("IDN:0027", $"Yüklenen dosyanın boyutu çok büyük.")
        {

        }
    }
}

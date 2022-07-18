namespace Patika.Shared.Exceptions.AccountDomain
{
    public class FormFileTypeIncorrectException : BaseApplicationException
    {
        public FormFileTypeIncorrectException() : base("IDN:0028", $"Lütfen png, jpg veya jpeg türünden bir dosya ekleyin.")
        {

        }
    }
}

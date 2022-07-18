namespace Patika.Shared.Exceptions.AccountDomain
{
    public class FormFileRequiredException : BaseApplicationException
    {
        public FormFileRequiredException() : base("IDN:0026", $"Lütfen dosya yükleyin.")
        {
        }
    }
}

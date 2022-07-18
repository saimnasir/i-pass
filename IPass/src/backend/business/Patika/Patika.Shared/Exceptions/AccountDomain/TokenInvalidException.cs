namespace Patika.Shared.Exceptions.AccountDomain
{
    public class TokenInvalidException : BaseApplicationException
    {
        public TokenInvalidException() : base("common:0002", $"Kullanıcı toke bilgisi alınamadı.")
        {

        }
    }
}


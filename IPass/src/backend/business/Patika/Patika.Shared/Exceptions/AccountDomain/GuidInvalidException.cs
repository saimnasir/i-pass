namespace Patika.Shared.Exceptions.AccountDomain
{
    public class GuidInvalidException : BaseApplicationException
    {
        public GuidInvalidException(string fieldName) : base("common:0018", $"{fieldName} alanı zorunludur.")
        {

        }
    }
}


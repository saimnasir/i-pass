namespace Patika.Shared.Exceptions.AccountDomain
{

    public class LastNameConsonantSequencialRepetitionException : BaseApplicationException
    {
        public LastNameConsonantSequencialRepetitionException() : base("IDN:0020", "Ad verisi içinde 4 sessiz harf yan yana bulunmamalıdır.")
        {

        }
    }
}



namespace Patika.Shared.Exceptions.AccountDomain
{

    public class FirstNameConsonantSequencialRepetitionException : BaseApplicationException
    {
        public FirstNameConsonantSequencialRepetitionException() : base("IDN:0012", "Ad verisi içinde 4 sessiz harf yan yana bulunmamalıdır.")
        {

        }
    }
}



namespace Patika.Shared.Exceptions.AccountDomain
{

    public class LastNameVowelSequencialRepetitionException : BaseApplicationException
    {
        public LastNameVowelSequencialRepetitionException() : base("IDN:0025", "Ad verisi içinde 3 sesli harf yan yana bulunmamalıdır.")
        {

        }
    }
}



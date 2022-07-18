namespace Patika.Shared.Exceptions.AccountDomain
{

    public class FirstNameVowelSequencialRepetitionException : BaseApplicationException
    {
        public FirstNameVowelSequencialRepetitionException() : base("IDN:0017", "Ad verisi içinde 3 sesli harf yan yana bulunmamalıdır.")
        {

        }
    }
}



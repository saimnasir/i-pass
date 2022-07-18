namespace Patika.Shared.Exceptions.AccountDomain
{

    public class FirstNameLetterSequencialRepetitionException : BaseApplicationException
    {
        public FirstNameLetterSequencialRepetitionException() : base("IDN:0014", "Ad verisi içinde aynı harften 3 veya daha fazlası yan yana bulunmamalıdır")
        {

        }
    }
}



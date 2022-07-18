namespace Patika.Shared.Exceptions.AccountDomain
{

    public class LastNameLetterSequencialRepetitionException : BaseApplicationException
    {
        public LastNameLetterSequencialRepetitionException() : base("IDN:0022", "Ad verisi içinde aynı harften 3 veya daha fazlası yan yana bulunmamalıdır")
        {

        }
    }
}



namespace Patika.Shared.Consts
{
    public static class RegexPatterns
    {
        /// <summary>
        /// Parola en az 6 karakterden oluşmalı, en az 1 harf ve en az 1 rakam içermelidir. Özel karakterler içerebilir
        /// </summary>
        public const string Password = "^(?=.*?[A-Za-z])(?=.*?[0-9])([#?!@$%^&*-]?).{6,}$";

        /// <summary>
        /// Telefon rakamlardan oluşan 10 karakter olmalı.
        /// (1111111,2222222 vs) ve ardışık numaralar(1234567, 2345678 vs)) gibi tekrarlı veiler içermemeli
        /// </summary>
        // TODO: change PhoneNumber regex
        public const string PhoneNumber = @"^\d{10}$";

        /// <summary>
        /// 5 lenght numeric
        /// </summary>
        public const string ActivationCode = @"^\d{5}$";       

        private const string MinLength = "^(.){n,}$";
        private const string NotStartWith = "^(?![n]).*";
        public const string SequencialRepetitionPattern = @"(.)\1\1";

        // public const string TurkishLetters = @"(?!_)\w"; // w: words and under score (excluded) => not works
        public const string TurkishLetters = "[abcçdefgğhıijklmnoöprsştuüvyzABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ]";
        public const string TurkishVowels = "[aeıioöuüAEIİOÖUÜ]";
        public const string TurkishConsonants = "[bcçdfgğhjklmnprsştvyzBCÇDFGĞHJKLMNPRSŞTVYZ]";

        public static string GetMinLengthPattern(int length)
        {
            return MinLength.Replace("n", length.ToString());
        }

        public static string GetNotStartWithPattern(string notStartWith)
        {
            return NotStartWith.Replace("n", notStartWith);
        }

        public static string GetOnlyLettersPattern()
        {
            return string.Concat(@"^", TurkishLetters, "+$");
        } 
        
        public static string GetVowelSequencialRepetitionPattern()
        {
            return string.Concat(@"\b(?=", TurkishLetters, "*", TurkishVowels, "{3})", TurkishLetters, @"+\b");
        }
        public static string GetConsonantSequencialRepetitionPattern()
        {
            return string.Concat(@"\b(?=", TurkishLetters, "*", TurkishConsonants, "{4})", TurkishLetters, @"+\b");
        }
         
    }
}

using System;
using System.Globalization;

namespace Patika.Shared.Extensions
{
    public static class TypeExtensions
    {
        public static string GetVerisonStringFromInt(this int version)
        {
            string tempVersion = version.ToString();
            string lastVersion = "";

            for (int i = 0; i < tempVersion.Length; i++)
            {
                if (!tempVersion[i].Equals('0'))
                {
                    lastVersion += tempVersion[i];

                    if (i + 1 != tempVersion.Length)
                    {
                        if (!tempVersion[i + 1].Equals('0'))
                            lastVersion += '.';
                    }
                    else
                        break;
                }
            }
            return lastVersion;
        }

        public static string GetTimeFormat(this string dateStr, bool isRequests, bool isMessage)
        {
            string result = "";

            string[] months = new string[] { "Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık" };
            DateTime date = Convert.ToDateTime(dateStr);
            TimeSpan diff = DateTime.Now - date; 

            int montDiff = Math.Abs((DateTime.Now.Month - date.Month) + 12 * (DateTime.Now.Year - date.Year));
            int yearDiff = montDiff / 12;
            int dayDiff = diff.Days;

            int ay = date.Month;
            int gun = date.Day;

            if (yearDiff == 0)  // is it current year
            {
                if (dayDiff >= 0 && dayDiff < 3)  // son 3 gun icerisinde
                {
                    if (dayDiff == 0)
                    {
                        if (DateTime.Now.Day - gun == 0)
                            result = "Bugün";
                        else if (DateTime.Now.Day - gun == 1)
                            result = "Dün";
                    }
                    else if (dayDiff == 1)
                        result = "Dün";
                    else if (dayDiff == 2)
                        result = "Önceki Gün";
                    else
                        result = gun.ToString() + " " + months[ay - 1];  // 01 Ağustos

                    if (!isRequests && !isMessage)  // talepler sayfasından mı
                        result += " " + date.ToShortTimeString();
                }
                else  // son 3 gun icerisinde degil
                {
                    result = gun.ToString() + " " + months[ay - 1];  // 01 Ağustos

                    if (!isRequests && !isMessage)
                        result += " " + date.ToShortTimeString();
                }
            }
            else if (yearDiff > 0)  // before current year
            {
                if (isRequests)
                    result = Convert.ToDateTime(dateStr).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);   // 01/08/1995
                else
                {
                    if (montDiff == 0)
                    {
                        if (dayDiff == 1)
                            result = "Dün";
                        else if (dayDiff == 2)
                            result = "Önceki Gün";
                        else if (dayDiff > 2 && dayDiff <= 7)
                            result = "1 hafta önce";
                        else if (dayDiff >= 8 && dayDiff <= 14)
                            result = "2 hafta önce";

                        if (dayDiff <= 2 && !isMessage)
                            result += " " + date.ToShortTimeString();
                    }
                    else if (montDiff >= 1 && montDiff <= 11)
                        result = montDiff.ToString() + " ay önce";
                    else if (montDiff >= 12 && montDiff <= 23)
                        result = yearDiff.ToString() + " yıl önce";
                    else
                        result = yearDiff.ToString() + " yıl önce";
                }
            }
            else
            {
                result = Convert.ToDateTime(dateStr).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);   // 01/08/1995
            }


            return result;
        }

        public static string ChangeTurToEng (this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }
            return text.Replace('ş', 's')
                       .Replace('Ş', 'S')
                       .Replace('ç', 'c')
                       .Replace('Ç', 'C')
                       .Replace('ı', 'i')
                       .Replace('İ', 'I')
                       .Replace('ğ', 'g')
                       .Replace('Ğ', 'G')
                       .Replace('ü', 'u')
                       .Replace('Ü', 'U')
                       .Replace('ö', 'o')
                       .Replace('Ö', 'O');
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patika.Shared.Extensions
{
    public static class EncodingHelper
    {
        public static string Encode(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            var textBytes = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(textBytes);
        }

        public static string Decode(this string base64)
        {
            if (string.IsNullOrEmpty(base64))
                return string.Empty;
            var base64Bytes = Convert.FromBase64String(base64);
            return Encoding.UTF8.GetString(base64Bytes);
        }

        public static double DecodeToDouble(this string source)
        {
            var decodeStr = source.Decode();
            if (!double.TryParse(decodeStr, out double value))
                return 0;
            return value;
        }
    }
}

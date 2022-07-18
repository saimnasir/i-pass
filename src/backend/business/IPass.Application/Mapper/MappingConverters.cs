using AutoMapper;
using IPass.Domain.CommonDomain.Entities;
using Patika.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPass.Application.Mapper
{
    public class BoolConverter : IValueConverter<bool, string>
    {
        public string Convert(bool source, ResolutionContext context)
            => source ? "YES" : "NO";
    }

    public class ReverseBoolConverter : IValueConverter<string, bool>
    {
        public bool Convert(string source, ResolutionContext context)
            => String.Equals(source, "YES", StringComparison.OrdinalIgnoreCase);
    }

    public class FinalConverter : IValueConverter<bool, string>
    {
        public string Convert(bool source, ResolutionContext context)
            => source ? "FİNAL" : "TASLAK";
    }

    public class DateFormatter : IValueConverter<DateTime?, string>
    {
        public string Convert(DateTime? source, ResolutionContext context)
            => source.HasValue ? source.Value.ToString("dd.MM.yyyy") : "";
    }
    public class StringToDoubleDecoder : IValueConverter<string, double>
    {
        public double Convert(string source, ResolutionContext context)
            => source.DecodeToDouble();
    }

    public class ExpirationConverter : IValueConverter<PinCode, bool>
    {
        public bool Convert(PinCode source, ResolutionContext context)
            => IsExpired(source);

        private bool IsExpired(PinCode pinCode)
        {
            var seconds = (long)DateTime.Now.Subtract(pinCode.Created).TotalSeconds;

            return seconds > pinCode.Expiration;
        }
    }

}

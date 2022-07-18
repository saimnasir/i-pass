using IPass.Domain.CommonDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPass.Application.Mapper
{
    public static class MapperExtensions
    {
        public static bool IsPinCodeExpired(this PinCode pinCode)
        {
            var seconds = (long)DateTime.Now.Subtract(pinCode.Created).TotalSeconds;

            return seconds > pinCode.Expiration;
        }
    }
}

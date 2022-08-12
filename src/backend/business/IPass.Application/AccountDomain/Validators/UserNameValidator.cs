using IPass.Application.Contracts.AccountDomain.Validators;
using IPass.Application.Contracts.CommonDomain.Validators;
using Patika.Shared.DTO.Validators;
using Patika.Shared.Exceptions.AccountDomain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPass.Application.AccountDomain.Validators
{
    public class UserNameValidator : IUserNameValidator
    {
        IRegexValidator RegexValidator { get; }

        public UserNameValidator(IRegexValidator regexValidator)
        {
            RegexValidator = regexValidator;
        }

        public async Task ValidateAsync(string input)
        {           
            if(string.IsNullOrEmpty(input))
            {
                throw new UserNameInvalidException();
            }           
            // not contains
            await RegexValidator.ValidateAsync(new RegexValidatorInput
            {
                Pattern = @"^((?![ıİçÇşŞğĞüÜöÖ]).)*$",
                Exception = new UserNameInvalidException(),
                Input = input
            });

            //"@, -, _, ." işaretleri ve harf rakam karakterleri dışındaki diğer karakterler olmamalıdır.
            await RegexValidator.ValidateAsync(new RegexValidatorInput
            {
                Pattern = @"^[a-zA-Z0-9\\_\\.\\-]+$",
                Exception = new UserNameInvalidException(),
                Input = input
            });

            //E-Mail verisi içinde ardarda iki nokta bulunmamalıdır.
            if (input.Contains(".."))
            {
                throw new UserNameInvalidException();
            }

            // "@, -, _, ." işaretlerinin çıkartılmasından sonra, uzunluğu 5 karakterden az olmamalıdır.
            List<char> charsToRemove = new() { '_', ',', '.' };
            string temp = Filter(input, charsToRemove);
            if (temp.Length < 5)
            {
                throw new UserNameInvalidException();
            }

            //email en fazla 254 karakter olmalı
            if (input.Length > 100)
            {
                throw new UserNameInvalidException();
            }

            await Task.FromResult(0);
        }

        private static string Filter(string str, List<char> charsToRemove)
        {
            foreach (char c in charsToRemove)
            {
                str = str.Replace(c.ToString(), String.Empty);
            }

            return str;
        }
    }
}

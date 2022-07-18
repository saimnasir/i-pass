using IPass.Application.Contracts.AccountDomain.Validators;
using IPass.Application.Contracts.CommonDomain.Validators;
using Patika.Shared.DTO.Validators;
using Patika.Shared.Exceptions.AccountDomain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPass.Application.AccountDomain.Validators
{
    public class EmailValidator : IEmailValidator
    {
        IRegexValidator RegexValidator { get; }

        public EmailValidator(IRegexValidator regexValidator)
        {
            RegexValidator = regexValidator;
        }

        public async Task ValidateAsync(string input)
        {           
            if(string.IsNullOrEmpty(input))
            {
                throw new EmailFormatInvalidException();
            }
            // is email
            // E-Mail verisi içinde birden fazla ‘@’ karakteri bulunmamalıdır.
            await RegexValidator.ValidateAsync(new RegexValidatorInput
            {
                Pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$",
                Exception = new EmailFormatInvalidException(),
                Input = input
            });
            // not contains
            await RegexValidator.ValidateAsync(new RegexValidatorInput
            {
                Pattern = @"^((?![ıİçÇşŞğĞüÜöÖ]).)*$",
                Exception = new EmailFormatInvalidException(),
                Input = input
            });

            //"@, -, _, ." işaretleri ve harf rakam karakterleri dışındaki diğer karakterler olmamalıdır.
            await RegexValidator.ValidateAsync(new RegexValidatorInput
            {
                Pattern = @"^[a-zA-Z0-9\\_\\.\\@\\-]+$",
                Exception = new EmailFormatInvalidException(),
                Input = input
            });

            //E-Mail verisi '.' ve '@' Karakteri ile başlamamalıdır
            if (input[0] == '.' || input[0] == '@')
            {
                throw new EmailFormatInvalidException();
            }

            //E-Mail verisi '.' ve '@' Karakteri ile bitmemelidir.
            if (input[^1] == '.' || input[^1] == '@')
            {
                throw new EmailFormatInvalidException();
            }

            //E-Mail verisi içinde “.@” veya “@.” karakterleri yan yana bulunmamalıdır.
            if (input.Contains(".@") || input.Contains("@.") || input.Contains(".."))
            {
                throw new EmailFormatInvalidException();
            }

            //E-Mail verisi içinde ardarda iki nokta bulunmamalıdır.
            if (input.Contains(".."))
            {
                throw new EmailFormatInvalidException();
            }

            // "@, -, _, ." işaretlerinin çıkartılmasından sonra, uzunluğu 5 karakterden az olmamalıdır.
            List<char> charsToRemove = new() { '@', '_', ',', '.' };
            string temp = Filter(input, charsToRemove);
            if (temp.Length < 5)
            {
                throw new EmailFormatInvalidException();
            }

            // @ işaretinden sonra En az 1 nokta işareti olmalıdır. 
            if (!input.Split('@')[1].Contains('.'))
            {
                throw new EmailFormatInvalidException();
            }

            //email en fazla 254 karakter olmalı
            if (input.Length > 254)
            {
                throw new EmailFormatInvalidException();
            }

            //domain en fazla 64 karakter olmalı
            if(input.Split('@')[1].Length > 64)
            {
                 throw new EmailFormatInvalidException();
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

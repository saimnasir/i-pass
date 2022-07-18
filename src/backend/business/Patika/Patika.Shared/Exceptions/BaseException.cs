using System;
using System.Collections;

namespace Patika.Shared.Exceptions
{
    public abstract class BaseException : Exception
    {
        public string Code { get; set; }       

        public BaseException(string code)
        {
            Code = code;
        }
        public BaseException(string code, string message): base(message)
        {
            Code = code;            
        }

        /// <summary>
        /// Ignored fields on Json Result
        /// </summary>

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public override string StackTrace { get; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public override IDictionary Data { get; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public override string HelpLink { get; set; }
    }
}

using Newtonsoft.Json;
using Patika.Shared.Enums;
using Patika.Shared.Exceptions;
using System;
using System.Text.Json.Serialization;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;

namespace Patika.Shared.DTO
{
    public class GeneralResponseDTO<T> where T : class
    {
        public virtual LogStatus ResultCode { get; set; } = LogStatus.Success;
        public virtual Exceptions.ApplicationException Exception { get; set; }
        public virtual bool Done { get => ResultCode == LogStatus.Success; }
        public virtual string Message { get; set; }
        public virtual Guid JobId { get; set; }
        public static GeneralResponseDTO<T> SuccessResponse(string message = "") => SuccessResponse(null, message, null);
        public static GeneralResponseDTO<T> SuccessResponse(T result, string message = "", Guid? jobId = null) => new() { Message = message, Result = result, ResultCode = LogStatus.Success , JobId = jobId ?? Guid.Empty};
        public static GeneralResponseDTO<T> ErrorResponse(string error = "", Guid? jobId = null) => new() { ResultCode = LogStatus.Exception, Message = error, JobId = jobId ?? Guid.Empty };

        public virtual T Result { get; set; }

        public static implicit operator GeneralResponseDTO<T>(T data) => SuccessResponse(data);

        public static implicit operator GeneralResponseDTO<T>(BaseFatalException fatalEx)
        {
            return new GeneralResponseDTO<T>
            {
                Message = fatalEx.Message,
                Result = null,
                ResultCode = LogStatus.InternalError,
                Exception = new Exceptions.ApplicationException(fatalEx)
            };
        }

        public static implicit operator GeneralResponseDTO<T>(BaseApplicationException applicationException)
        {
            return new GeneralResponseDTO<T>
            {
                Message = applicationException.Message,
                Result = null,
                ResultCode = LogStatus.Exception,
                Exception = new Exceptions.ApplicationException(applicationException)
            };
        }

        public static implicit operator GeneralResponseDTO<T>(BaseSystemException systemException)
        {
            return new GeneralResponseDTO<T>
            {
                Message = systemException.Message,
                Result = null,
                ResultCode = LogStatus.InternalError,
                Exception = new Exceptions.ApplicationException(systemException)
            };
        }

        public static implicit operator GeneralResponseDTO<T>(Exception exception)
        {
            if (exception is BaseApplicationException)
                return exception as BaseApplicationException;
            else if (exception is BaseFatalException)
                return exception as BaseFatalException;
            else if (exception is BaseSystemException)
                return exception as BaseSystemException;
            else
                return new GeneralResponseDTO<T>
                {
                    Message = exception.Message,
                    Result = null,
                    ResultCode = LogStatus.Exception,
                    Exception = new GeneralException(exception)
                };
        }

        protected string WellKnownErrorTranslation(string msg)
		{
            if (msg.StartsWith("A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond."))
                return "Servise şu anda erişilemiyor";
            else if (msg.Contains("Unexpected character encountered while parsing value:"))
                return "Response is not valid";
            return msg;
		}

        public static implicit operator GeneralResponseDTO<T>(string error) => new Exception(error);
    }
    public class GeneralResponseDTO : GeneralResponseDTO<object> { }
    public class FinalResponseDTO<T> : GeneralResponseDTO<T> where T : class
    {
        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public override Exceptions.ApplicationException Exception { get; set; }
        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public override LogStatus ResultCode { get; set; } = LogStatus.Success;
        [JsonProperty("success")]
        [JsonPropertyName("success")]
        public override bool Done { get => ResultCode == LogStatus.Success; }
        public override string Message { get; set; }
        [JsonProperty("logId")]
        [JsonPropertyName("logId")]
        public override Guid JobId { get; set; }
        [JsonProperty("data")]
        [JsonPropertyName("data")]
        public override T Result { get; set; }
        public FinalResponseDTO(GeneralResponseDTO<T> general)
        {
            JobId = general.JobId;
            Exception = general.Exception;
            ResultCode = general.ResultCode;
            Result = general.Result;
            Message = WellKnownErrorTranslation(general.Message);
            JobId = general.JobId;
        }
    }
}
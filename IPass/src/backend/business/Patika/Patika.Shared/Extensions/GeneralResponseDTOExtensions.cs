using Microsoft.AspNetCore.Mvc;
using Patika.Shared.DTO;
using Patika.Shared.Exceptions;
using System;
using ApplicationException = Patika.Shared.Exceptions.ApplicationException;

namespace Patika.Shared.Extensions
{
    public static class GeneralResponseDTOExtensions
    {
        public static ActionResult<GeneralResponseDTO<T>> ToGeneralResponse<T>(this T data, Guid? jobId = null) where T : class
        {
            var res = (GeneralResponseDTO<T>)data;
            res.JobId = jobId ?? Guid.Empty;
            return new ActionResult<GeneralResponseDTO<T>>(res);
        }

        public static ActionResult<GeneralResponseDTO<T>> ToExceptionGeneralResponse<T>(this Exception data, Guid? jobId = null) where T : class
        {
            var res = (GeneralResponseDTO<T>)data;
            res.JobId = jobId ?? Guid.Empty;
            return new ActionResult<GeneralResponseDTO<T>>(res);
        }

        public static BaseApplicationException ToBaseException<T>(this GeneralResponseDTO<T> exceptionResponse) where T: class
		{
            if(exceptionResponse.Exception != null)
                return new ApplicationException(exceptionResponse.Exception);
            throw new InvalidOperationException();
        }
    }
}
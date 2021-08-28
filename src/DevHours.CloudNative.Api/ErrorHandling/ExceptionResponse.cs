/*
 * Consumed and adapted from: https://github.com/snatch-dev/Convey/blob/master/src/Convey.WebApi/src/Convey.WebApi/Exceptions/ExceptionResponse.cs
 */
using System.Net;

namespace DevHours.CloudNative.Api.ErrorHandling
{
    public class ExceptionResponse
    {
        public string ErrorCode { get; set; }
        public string Message { get; set; }
    }
}
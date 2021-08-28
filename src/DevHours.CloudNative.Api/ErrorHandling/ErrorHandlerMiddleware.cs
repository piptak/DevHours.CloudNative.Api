/*
 * Consumed and adapted from: https://github.com/snatch-dev/Convey/blob/master/src/Convey.WebApi/src/Convey.WebApi/Exceptions/ErrorHandlerMiddleware.cs
 */
using AutoMapper;
using DevHours.CloudNative.Core.Exceptions;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace DevHours.CloudNative.Api.ErrorHandling
{
    internal sealed class ErrorHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlerMiddleware> logger;
        private ConcurrentDictionary<string, string> _errorCodes = new ConcurrentDictionary<string, string>();

        public ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger)
        {
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, exception.Message);
                await HandleErrorAsync(context, exception);
            }
        }

        private async Task HandleErrorAsync(HttpContext context, Exception exception)
        {
            string message = "An error occurred.";
            string errorCode = "error";
            int statusCode = StatusCodes.Status500InternalServerError;

            if (exception is DomainException)
            {
                if (!_errorCodes.TryGetValue(exception.GetType().Name, out errorCode))
                {
                    errorCode = exception.GetType().Name.Underscore().Replace("_exception", string.Empty);
                    _errorCodes.TryAdd(exception.GetType().Name, errorCode);
                }

                statusCode = StatusCodes.Status400BadRequest;
                message = exception.Message;
            }

            context.Response.StatusCode = statusCode;
            var error = new ExceptionResponse()
            {
                Message = message,
                ErrorCode = errorCode
            };
            await context.Response.WriteAsJsonAsync(error, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
    }
}
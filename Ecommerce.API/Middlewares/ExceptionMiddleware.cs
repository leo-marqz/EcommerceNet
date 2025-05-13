using Ecommerce.Api.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SendGrid.Helpers.Errors.Model;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Ecommerce.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";

                int statusCode = (int) HttpStatusCode.InternalServerError;
                string result = string.Empty;

                switch (ex)
                {
                    case NotFoundException notFoundException:
                        statusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case FluentValidation.ValidationException validationException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        var errors = validationException.Errors
                                        .Select( ers => ers.ErrorMessage )
                                        .ToArray();
                        var validationJsons = JsonConvert.SerializeObject(errors);
                        result = JsonConvert.SerializeObject(
                                new CodeErrorException(statusCode, errors, validationJsons)
                                );
                        break;
                    case BadHttpRequestException badHttpRequestException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        statusCode = (int)HttpStatusCode.InternalServerError;
                        break;

                }

                if (string.IsNullOrEmpty(result))
                {
                    result = JsonConvert.SerializeObject(
                        new CodeErrorException(
                                statusCode, 
                                new string[] { ex.Message },
                                ex.StackTrace
                            )
                        );
                }

                context.Response.StatusCode = statusCode;
                await context.Response.WriteAsync(result);

            }
        }
    }
}

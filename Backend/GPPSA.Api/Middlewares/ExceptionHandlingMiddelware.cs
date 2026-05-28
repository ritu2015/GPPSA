using System.Text.Json;
using GPPSA.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace GPPSA.Api.Middlewares
{
    public class ExceptionHandlingMiddelware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddelware> _logger;
        public ExceptionHandlingMiddelware(RequestDelegate next, ILogger<ExceptionHandlingMiddelware> logger)
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
                _logger.LogError(ex, "Unhandled Exception: {Message}", ex.Message);
                await HandleExceptionAsync(context,ex );
            }
            
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var problemDetails=new ProblemDetails // this is the RFC-7807 compliant way of returning errors in REST APIs
            {
                Instance = context.Request.Path
            };
            
            switch (exception)
            {
                case NotFoundException:
                    problemDetails.Title = "Resource Not Found";
                    problemDetails.Status = StatusCodes.Status404NotFound;
                    problemDetails.Detail = exception.Message;
                    break;
                case BadRequestException:
                    problemDetails.Title = "Bad Request";
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Detail = exception.Message;
                    break;
                default:
                    problemDetails.Title = "Internal Server Error";
                    problemDetails.Status = StatusCodes.Status500InternalServerError;
                    problemDetails.Detail = "An unexpected error occurred. Please try again later.";
                    break;
            }
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode= problemDetails.Status.Value;
            return context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
        }
    }
}
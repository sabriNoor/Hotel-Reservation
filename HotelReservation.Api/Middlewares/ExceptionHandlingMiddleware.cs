using System.Net;
using HotelReservation.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using BadHttpRequestException = HotelReservation.Application.Exceptions.BadHttpRequestException;
using UnauthorizedAccessException = HotelReservation.Application.Exceptions.UnauthorizedAccessException;

namespace HotelReservation.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (FluentValidation.ValidationException ex)
            {
                throw new ValidationException(ex.Errors);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode statusCode;
            string title;
            string detail = exception.Message;

            switch (exception)
            {
                case BadHttpRequestException:
                case BusinessException:
                case ValidationException:
                    statusCode = HttpStatusCode.BadRequest;
                    title = "Bad Request";
                    break;
                case ConflictException:
                    statusCode = HttpStatusCode.Conflict;
                    title = "Conflict";
                    break;
                case NotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    title = "Not Found";
                    break;
                case UnauthorizedAccessException:
                    statusCode = HttpStatusCode.Unauthorized;
                    title = "Unauthorized";
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    title = "Internal Server Error";
                    break;
            }

            var problemDetails = new ProblemDetails
            {
                Status = (int)statusCode,
                Title = title,
                Detail = detail,
                Type = exception.GetType().Name
            };

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/problem+json";
            return context.Response.WriteAsJsonAsync(problemDetails);
        }
    }

    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}

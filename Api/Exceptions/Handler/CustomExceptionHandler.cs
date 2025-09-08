﻿using Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Api.Exceptions.Handler
{
    public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
    {
        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogWarning("Обработанное исключение: {Message}, время: {time}", exception.Message, DateTime.Now);

            (string Detail, string Title, int StatusCode) details = exception switch
            {
                NotFoundException =>(
                exception.Message, 
                exception.GetType().Name, 
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound
                ),
                _=>(
                exception.Message, 
                exception.GetType().Name,
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError
                )
            };

            var problemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails 
            { 
                Title = details.Title,
                Detail = details.Detail,
                Status = details.StatusCode,
                Instance = httpContext.
            };

        }
    }
}

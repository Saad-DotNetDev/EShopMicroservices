using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DeviceFarm.Model;
using Amazon.ElasticMapReduce.Model;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingBlockks.Exceptions.Handler
{
    public class CustomExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext context,
            Exception exception,
            CancellationToken cancellationToken)
        {
            (string Detail, string Title, int StatusCode) details = exception switch
            {
                InternalServerException =>
                    (
                        exception.Message,
                        exception.GetType().Name,
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError
                    ),

                FluentValidation.ValidationException =>
                    (
                        exception.Message,
                        exception.GetType().Name,
                        context.Response.StatusCode = StatusCodes.Status400BadRequest
                    ),

                BadRequestException =>
                    (
                        exception.Message,
                        exception.GetType().Name,
                        context.Response.StatusCode = StatusCodes.Status400BadRequest
                    ),

                NotFoundException =>
                    (
                        exception.Message,
                        exception.GetType().Name,
                        context.Response.StatusCode = StatusCodes.Status404NotFound
                    ),

                _ =>
                    (
                        exception.Message,
                        exception.GetType().Name,
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError
                    )
            };

            var problemDetail = new ProblemDetails
            {
                Title = details.Title,
                Detail = details.Detail,
                Status = details.StatusCode,
                Instance = context.Request.Path
            };

            problemDetail.Extensions.Add("TraceId", context.TraceIdentifier);

            if (exception is FluentValidation.ValidationException validationException)
            {
                problemDetail.Extensions.Add("ValidationErrors", validationException.Errors);
            }

            await context.Response.WriteAsJsonAsync(problemDetail, cancellationToken);

            return true;  
        }
    }
}

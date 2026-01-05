using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingBlockks.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>
        (ILogger<LoggingBehavior<TRequest,TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull ,IRequest<TResponse>
        where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("[Start] Handle request={Request} - Response={Response}",typeof(TRequest).Name,typeof(TResponse).Name,request);

            var timer = new Stopwatch();
            timer.Start();

            var response = await next();
            timer.Stop();

            var timeTaken = timer.Elapsed;
            if (timeTaken.Seconds > 3) 
                    logger.LogWarning("[Performance] The request {request} took {timeTaken} seconds.",
                        typeof(TRequest).Name, timeTaken.Seconds);


            logger.LogInformation("[End] Handle {Request} with {Response} ", typeof(TRequest).Name, typeof(TResponse).Name, request);
                    return response;

            }

        }
    }


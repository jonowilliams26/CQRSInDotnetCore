using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSTest.Behaviours
{
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> logger;

        public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
        {
            this.logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            // Pre logic
            logger.LogInformation("{Request} is starting.", request.GetType());
            var timer = Stopwatch.StartNew();
            var response = await next();
            timer.Stop();
            // Post logic
            logger.LogInformation("{Request} has finished in {Time}ms.", request.GetType(), timer.ElapsedMilliseconds);

            return response;
        }
    }
}

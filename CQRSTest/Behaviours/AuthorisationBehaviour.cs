using CQRSTest.Authorisation;
using CQRSTest.Authorisation.Requirements;
using CQRSTest.DTOs;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSTest.Behaviours
{
    public class AuthorisationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
        where TRequest : IAuthorisable
        where TResponse : CQRSResponse, new()
    {
        private readonly ILogger<AuthorisationBehaviour<TRequest, TResponse>> logger;
        private readonly IServiceProvider serviceProvider;

        public AuthorisationBehaviour(ILogger<AuthorisationBehaviour<TRequest, TResponse>> logger,
                                      IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var requestName = request.GetType();
            logger.LogInformation("{Request} is authorisable.", requestName);
            
            // Loop through each requirement for the request and authorise
            foreach(var requirement in request.Requirements)
            {
                var handlerType = typeof(IRequirementHandler<>).MakeGenericType(requirement.GetType());
                var handler = serviceProvider.GetRequiredService(handlerType);
                var methodInfo = handler.GetType().GetMethod(nameof(IRequirementHandler<IRequirement>.Handle));
                var result = await (Task<AuthorisationResult>)methodInfo.Invoke(handler, new[] { requirement });
                if (result.IsAuthorised)
                {
                    logger.LogInformation("{Requirement} has been authorised for {Request}.", requirement.GetType().Name, requestName);
                    continue;
                }

                logger.LogWarning("{Requirement} FAILED for {Request}.", requirement.GetType().Name, requestName);
                return new TResponse { StatusCode = HttpStatusCode.Unauthorized };
            }

            logger.LogInformation("{Request} authorisation was successful.", requestName);
            return await next();
        }
    }
}

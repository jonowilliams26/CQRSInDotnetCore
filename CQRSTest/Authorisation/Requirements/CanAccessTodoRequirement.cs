using CQRSTest.Authorisation.Requirements;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CQRSTest.Authorisation.Requirements
{
    public record CanAccessTodoRequirement(int Id) : IRequirement;
    public class CanAccessTodoRequirementHandler : IRequirementHandler<CanAccessTodoRequirement>
    {
        private readonly ILogger<CanAccessTodoRequirementHandler> logger;

        public CanAccessTodoRequirementHandler(ILogger<CanAccessTodoRequirementHandler> logger)
        {
            this.logger = logger;
        }

        public async Task<AuthorisationResult> Handle(CanAccessTodoRequirement requirement)
        {
            logger.LogInformation("User has access to todo item. ID: {Id}", requirement.Id);
            return AuthorisationResult.Authorised;
        }
    }
}

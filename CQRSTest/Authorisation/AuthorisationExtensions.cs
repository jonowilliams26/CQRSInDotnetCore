using CQRSTest.Authorisation.Requirements;
using Microsoft.Extensions.DependencyInjection;

namespace CQRSTest.Authorisation
{
    public static class AuthorisationExtensions
    {
        public static void AddAuthorisationRequirementHandlers(this IServiceCollection services)
        {
            services.Scan(scan => scan
              .FromAssemblyOf<IRequirementHandler>()
                .AddClasses(classes => classes.AssignableTo<IRequirementHandler>())
                  .AsImplementedInterfaces()
                  .WithTransientLifetime());
        }
    }
}

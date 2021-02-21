using System.Threading.Tasks;

namespace CQRSTest.Authorisation.Requirements
{
    public interface IRequirementHandler { }
    public interface IRequirementHandler<T> : IRequirementHandler where T : IRequirement
    {
        Task<AuthorisationResult> Handle(T requirement);
    }
}

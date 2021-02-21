using CQRSTest.Authorisation.Requirements;
using System.Collections.Generic;

namespace CQRSTest.Authorisation
{
    public interface IAuthorisable
    {
        List<IRequirement> Requirements { get; }
    }
}

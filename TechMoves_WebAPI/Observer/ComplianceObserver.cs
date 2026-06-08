using TechMove_Logistics.Models;
using TechMoves_WebAPI.Models;

namespace TechMoves_WebAPI.Observer
{
    public class ComplianceModule : IContractObserver
    {
        public void Update(Contract contract)
        {
            if (string.IsNullOrEmpty(contract.SignedAgreementPath))
            {
                throw new InvalidOperationException("Contract missing signed agreement.");
            }
        }
    }
}

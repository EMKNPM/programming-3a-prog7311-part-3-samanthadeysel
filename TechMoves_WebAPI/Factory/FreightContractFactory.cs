using TechMove_Logistics.Models;
using TechMoves_WebAPI.Models;

namespace TechMoves_WebAPI.Factory
{
    public class FreightContractFactory : IContractFactory
    {
        public Contract CreateContract()
        {
            return new Contract
            {
                ContractType = "Freight",
                ServiceLevel = "Standard",
                SignedAgreementPath = "freight_agreement.pdf"
            };
        }
    }
}

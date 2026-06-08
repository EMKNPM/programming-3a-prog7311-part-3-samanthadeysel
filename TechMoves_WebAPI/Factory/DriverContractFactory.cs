
using TechMoves_WebAPI.Models;

namespace TechMoves_WebAPI.Factory
{
    public class DriverContractFactory : IContractFactory
    {
        public Contract CreateContract()
        {
            return new Contract
            {
                ContractType = "Driver",
                ServiceLevel = "Premium",
                SignedAgreementPath = "driver_agreement.pdf"
            };
        }
    }
}

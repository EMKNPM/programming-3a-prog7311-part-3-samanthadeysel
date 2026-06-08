
using TechMoves_WebAPI.Models;

namespace TechMoves_WebAPI.Observer
{
    public class ServiceRequestModule : IContractObserver
    {
        public void Update(Contract contract)
        {
            Console.WriteLine($"ServiceRequestModule notified: Contract {contract.ContractId} updated.");
        }
    }
}



using TechMoves_WebAPI.Models;

namespace TechMoves_WebAPI.State
{
    public class ExpiredState : ContractState
    {
        public override string Name => "Expired";

        public override void HandleRequest(Contract contract)
        {
            contract.Status = Name;
        }
    }
}

using TechMove_Logistics.Models;

namespace TechMoves_WebAPI.State
{
    public class DraftState : ContractState
    {
        public override string Name => "Draft";

        public override void HandleRequest(Contract contract)
        {
            contract.Status = Name;
        }
    }
}

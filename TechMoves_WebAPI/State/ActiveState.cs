

using TechMoves_WebAPI.Models;

namespace TechMoves_WebAPI.State
{
    public class ActiveState : ContractState
    {
        public override string Name => "Active";

        public override void HandleRequest(Contract contract)
        {
            contract.Status = Name;
        }
    }
}

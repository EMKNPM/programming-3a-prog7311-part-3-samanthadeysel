

using TechMoves_WebAPI.Models;

namespace TechMoves_WebAPI.State
{
    public abstract class ContractState
    {
        public abstract string Name { get; }

        public abstract void HandleRequest(Contract contract);
    }
}

using System.ComponentModel.DataAnnotations;
using TechMoves_WebAPI.Observer;
using TechMoves_WebAPI.State;

namespace TechMoves_WebAPI.Models
{
    public class Contract
    {

        public int ContractId { get; set; }
        [Required]
        public int ClientId { get; set; }

        [Required]
        public string ContractName { get; set; } = string.Empty;

        [Required]
        public string ContractDescription { get; set; } = string.Empty;

        [Required]
        public string ContractType { get; set; } = string.Empty;

        [Required]
        public string Status { get; set; } = "Draft";

        [Required]
        public string ServiceLevel { get; set; } = string.Empty;

        [Required]
        public string SignedAgreementPath { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Client? Client { get; set; }

        public ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();

        public ContractState? State { get; private set; }
        public void SetState(ContractState state)
        {
            State = state;
            state.HandleRequest(this);
            Notify();
        }

        private readonly List<IContractObserver> _observers = new();

        public void Attach(IContractObserver observer) => _observers.Add(observer);
        public void Detach(IContractObserver observer) => _observers.Remove(observer);
        public void Notify()
        {
            foreach (var observer in _observers)
                observer.Update(this);
        }
    }
}

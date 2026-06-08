using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // 🟢 Added for NotMapped attribute
using TechMoves_WebAPI.Observer;
using TechMoves_WebAPI.State;

namespace TechMoves_WebAPI.Models
{
    public class Contract
    {
        public Contract()
        {
            _observers = new List<IContractObserver>
            {
                new ComplianceModule(),
                new ServiceRequestModule()
            };
        }

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

        [NotMapped]
        public ContractState? State { get; private set; }

        public void SetState(ContractState state)
        {
            if (!string.IsNullOrEmpty(this.SignedAgreementPath))
            {
                TechMoves_WebAPI.Utilities.FileValidator.Validate(this.SignedAgreementPath);
            }

            State = state;
            state.HandleRequest(this);
            Notify();
        }

        [NotMapped]
        private readonly List<IContractObserver> _observers;

        public void Attach(IContractObserver observer) => _observers.Add(observer);
        public void Detach(IContractObserver observer) => _observers.Remove(observer);
        public void Notify()
        {
            foreach (var observer in _observers)
                observer.Update(this);
        }
    }
}

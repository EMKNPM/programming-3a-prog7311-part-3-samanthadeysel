namespace TechMove_Logistics.ViewModels
{
    public class ContractViewModel
    {
        public int ContractId { get; set; }
        public int ClientId { get; set; }

        public string ContractName { get; set; } = string.Empty;
        public string ContractDescription { get; set; } = string.Empty;
        public string ContractType { get; set; } = string.Empty;
        public string Status { get; set; } = "Draft";
        public string ServiceLevel { get; set; } = string.Empty;
        public string SignedAgreementPath { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string? ClientName { get; set; }
        public string? ClientRegion { get; set; }

        public string Duration => $"{(EndDate - StartDate).Days} days";
        public bool IsActive => Status == "Active";
        public bool IsExpired => EndDate < DateTime.Now;

        public string Summary => $"{ContractName} ({Status}) - {ServiceLevel}";
    }
}

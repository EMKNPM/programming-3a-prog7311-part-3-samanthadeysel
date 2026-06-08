namespace TechMove_Logistics.ViewModels
{
    public class ServiceRequestViewModel
    {
        public int ServiceRequestId { get; set; }
        public string Status { get; set; } = "Open";
        public string RequestType { get; set; } = string.Empty;
        public DateTime RequestDate { get; set; } = DateTime.Now;
        public int ContractId { get; set; }
        public string Description { get; set; } = string.Empty;

        public decimal CostUSD { get; set; }
        public decimal CostZAR { get; set; }   

        public string? ContractName { get; set; }
        public string? ClientName { get; set; }

        public string RequestDateFormatted => RequestDate.ToString("dd MMM yyyy");
        public string FormattedCostUSD => $"${CostUSD:N2}";
        public string FormattedCostZAR => CostZAR > 0 ? $"R{CostZAR:N2}" : "N/A";

        public bool IsPending => Status == "Open";
        public bool IsCompleted => Status == "Completed";

        // Display-friendly summary
        public string Summary => $"{RequestType} - {Status} ({FormattedCostUSD})";
    }
}

namespace TechMove_Logistics.ViewModels
{
    public class PaymentViewModel
    {
        public int PaymentId { get; set; }
        public int ContractId { get; set; }

        public decimal AmountUSD { get; set; }
        public decimal AmountZAR { get; set; }
        public DateTime PaymentDate { get; set; }

        public string? ContractName { get; set; }
        public string? ClientName { get; set; }

        public string FormattedUSD => $"${AmountUSD:N2}";
        public string FormattedZAR => $"R{AmountZAR:N2}";
        public string PaymentDateFormatted => PaymentDate.ToString("dd MMM yyyy");

        public string Summary => $"{FormattedUSD} ({FormattedZAR}) on {PaymentDateFormatted}";
    }
}

using System.ComponentModel.DataAnnotations;

namespace TechMoves_WebAPI.Models
{
    public class ServiceRequest
    {
        public int ServiceRequestId { get; set; }

        [Required]
        public string Status { get; set; } = "Open";

        [Required]
        public string RequestType { get; set; } = string.Empty;

        public DateTime RequestDate { get; set; } = DateTime.Now;

        [Required]
        public int ContractId { get; set; }

        public string Description { get; set; } = string.Empty;

        public Contract? Contract { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal CostUSD { get; set; }
    }

}

using System.ComponentModel.DataAnnotations;

namespace TechMoves_WebAPI.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }

        [Required]
        public int ContractId { get; set; }
        public Contract? Contract { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal AmountUSD { get; set; }

        [DataType(DataType.Currency)]
        public decimal AmountZAR { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.Now;
    }
}

namespace TechMove_Logistics.ViewModels
{
    public class ClientViewModel
    {
        public int ClientId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Region { get; set; } = string.Empty;

        public string ContactNumber { get; set; } = string.Empty;

        public string DisplayInfo => $"{Name} ({Region})";

        public string MaskedContact => ContactNumber.Length > 4
            ? new string('*', ContactNumber.Length - 4) + ContactNumber[^4..]
            : ContactNumber;
    }
}

namespace TechMove_Logistics.ViewModels
{
    public class ManagerViewModel
    {
        public int ManagerId { get; set; }
        public int ServiceRequestId { get; set; }

        public string? ServiceRequestDescription { get; set; }
        public string? ServiceRequestStatus { get; set; }

        public string Summary => $"Manager #{ManagerId} linked to ServiceRequest #{ServiceRequestId}";
    }
}

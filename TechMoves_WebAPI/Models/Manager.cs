namespace TechMoves_WebAPI.Models
{
    public class Manager
    {
        public int ManagerId { get; set; }
        public int ServiceRequestId { get; set; }
        public void RaiseRequest() { /* logic */ }
    }
}

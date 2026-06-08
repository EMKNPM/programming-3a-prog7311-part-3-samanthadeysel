namespace TechMove_Logistics.ViewModels
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }

        // Extra properties for the view only
        public string FullName => $"{FirstName} {LastName}";
    }
}
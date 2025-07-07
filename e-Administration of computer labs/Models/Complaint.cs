
    namespace e_Administration_of_computer_labs.Models

{
    public class Complaint
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime DateSubmitted { get; set; } // instead of CreatedAt
        public string Status { get; set; }
        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public int LabId { get; set; }
        public Lab? Lab { get; set; }

        public int EquipmentId { get; set; }
        public Equipment? Equipment { get; set; }
    }
}

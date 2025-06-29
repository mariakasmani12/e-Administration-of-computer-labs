namespace e_Administration_of_computer_labs.Models
{
    public class Complaint
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DateSubmitted { get; set; }
        public string Status { get; set; } // Pending, In Progress, Resolved

        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public int LabId { get; set; }
        public Lab? Lab { get; set; } = null!;


        public int EquipmentId { get; set; }
        public Equipment? Equipment { get; set; }
    }
}

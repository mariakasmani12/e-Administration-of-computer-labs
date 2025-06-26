namespace e_Administration_of_computer_labs.Models
{
    public class Complaint
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DateSubmitted { get; set; }
        public string Status { get; set; } // Pending, In Progress, Resolved

        public int UserId { get; set; }
        public User User { get; set; }

        public int LabId { get; set; }
        public Lab Lab { get; set; }


        public int? EquipmentId { get; set; }
        public Equipment Equipment { get; set; }
        public ICollection<Report> Reports { get; set; }
    }
}

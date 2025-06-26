namespace e_Administration_of_computer_labs.Models
{
    public class Report
    {
        public int Id { get; set; }
        public int? ComplaintId { get; set; }
        public Complaint Complaint { get; set; }

        public int? SoftwareId { get; set; }
        public Software Software { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public string Remarks { get; set; }
        public DateTime ReportDate { get; set; }
    }
}

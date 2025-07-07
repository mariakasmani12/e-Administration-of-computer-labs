namespace e_Administration_of_computer_labs.Models
{
    public class LabReport
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime SubmittedAt { get; set; }

        public string InstructorId { get; set; }

        public ApplicationUser Instructor { get; set; }

        public int LabId { get; set; }

        public Lab Lab { get; set; }
    }
}

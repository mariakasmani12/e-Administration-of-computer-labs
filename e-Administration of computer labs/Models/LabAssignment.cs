namespace e_Administration_of_computer_labs.Models
{
    public class LabAssignment
    {
        public int Id { get; set; }
        public int LabId { get; set; }
        public Lab? Lab { get; set; }

        public string? InstructorId { get; set; }
        public ApplicationUser? Instructor { get; set; }

        public string DayOfWeek { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}

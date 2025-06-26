namespace e_Administration_of_computer_labs.Models
{
    public class LabAssignment
    {
        public int Id { get; set; }
        public int LabId { get; set; }
        public Lab Lab { get; set; }

        public int InstructorId { get; set; }
        public User Instructor { get; set; }

        public string DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}

namespace e_Administration_of_computer_labs.Models
{
    public class ExtraLabRequest
    {

        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public int HODId { get; set; }
        public User HOD { get; set; }

        public int LabId { get; set; }
        public Lab Lab { get; set; }

        public DateTime RequestDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Status { get; set; }
    }
}

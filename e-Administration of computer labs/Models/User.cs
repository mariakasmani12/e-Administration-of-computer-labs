namespace e_Administration_of_computer_labs.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public ICollection<Complaint> Complaints { get; set; }
        public ICollection<LabAssignment> LabAssignments { get; set; }
        public ICollection<Report> Reports { get; set; }
    }
}

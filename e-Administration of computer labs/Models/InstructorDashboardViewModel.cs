using System.Collections.Generic;

namespace e_Administration_of_computer_labs.Models
{
    public class InstructorDashboardViewModel
    {
        public List<LabAssignment> AssignedLabs { get; set; } = new();
        public List<Complaint> RecentComplaints { get; set; } = new();
    }
}

// =============================================
// ✅ Step 2: HODDashboardViewModel.cs
// =============================================

using System.Collections.Generic;

namespace e_Administration_of_computer_labs.Models
{
    public class HODDashboardViewModel
    {
        public List<Complaint> DepartmentComplaints { get; set; } = new();
        public List<ExtraLabRequest> MyLabRequests { get; set; } = new();
    }
}
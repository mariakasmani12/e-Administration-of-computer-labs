namespace e_Administration_of_computer_labs.Models
{
    public class AssignRoleDepartmentViewModel
    {
        public string UserId { get; set; }
        public string? UserEmail { get; set; }

        public int? SelectedRoleId { get; set; }
        public int? SelectedDepartmentId { get; set; }

        public List<Role> Roles { get; set; } = new();
        public List<Department> Departments { get; set; } = new();
    }
}

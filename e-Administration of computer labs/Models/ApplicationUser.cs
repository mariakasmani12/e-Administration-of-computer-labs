using Microsoft.AspNetCore.Identity;

namespace e_Administration_of_computer_labs.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }

        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }

        public int? RoleId { get; set; }
        public Role? Role { get; set; }

    }
}

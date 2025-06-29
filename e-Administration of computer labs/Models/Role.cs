namespace e_Administration_of_computer_labs.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
    }

}

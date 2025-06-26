namespace e_Administration_of_computer_labs.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
        public ICollection<ExtraLabRequest> ExtraLabRequests { get; set; }
    }
}

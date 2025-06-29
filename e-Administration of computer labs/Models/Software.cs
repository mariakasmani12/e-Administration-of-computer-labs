namespace e_Administration_of_computer_labs.Models
{
    public class Software
    {
         public int Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public DateTime InstallationDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string DocumentationLink { get; set; }
    }
}

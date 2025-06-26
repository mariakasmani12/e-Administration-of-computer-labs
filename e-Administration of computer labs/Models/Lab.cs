namespace e_Administration_of_computer_labs.Models
{
    public class Lab
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RoomNumber { get; set; }

        public ICollection<Equipment> Equipments { get; set; }
        public ICollection<Complaint> Complaints { get; set; }
        public ICollection<LabAssignment> LabAssignments { get; set; }
    }
}

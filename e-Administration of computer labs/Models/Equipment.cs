namespace e_Administration_of_computer_labs.Models
{
    public class Equipment
    {
       public int Id { get; set; }
    public string Name { get; set; }
    public string SerialNumber { get; set; }
    public DateTime WarrantyExpiryDate { get; set; }

    public int LabId { get; set; }
    public Lab Lab { get; set; }

     public ICollection<Complaint> Complaints { get; set; }

    }
}

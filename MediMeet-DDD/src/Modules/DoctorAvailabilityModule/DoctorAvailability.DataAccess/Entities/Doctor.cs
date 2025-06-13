namespace DoctorAvailability.DataAccess.Entities;

public class Doctor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Specialty { get; set; }
    public virtual ICollection<Slot> Slots { get; set; } = new List<Slot>();
}
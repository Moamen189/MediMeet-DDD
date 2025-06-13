namespace AppointmentBooking.Domain.DomainModels;

public class Patient
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    
    
    //factory method is better than constructor as it show intention
    public static Patient CreatePatient(int id,string patientName, string email, string phoneNumber)
    {
        var patient = new Patient
        {
            Id = id,
            Name = patientName,
            Email = email,
            PhoneNumber = phoneNumber
        };
        return patient;
    }
}
using AppointmentBooking.Infrastructure.Entities;

namespace AppointmentBooking.Infrastructure.Database;

public static class InMemoryDb
{
    public static List<PatientEntity> Patients = new()
    {
        new()
        {
            Id = 1,
            Name = "Ahmed Hassan",
            Email = "ahmed.hassan@example.com",
            PhoneNumber = "111-222-3333"
        },
        new()
        {
            Id = 2,
            Name = "Sara Ali",
            Email = "sara.ali@example.com",
            PhoneNumber = "222-333-4444"
        }
    };


    public static List<AppointmentEntity> Appointments = new();
}
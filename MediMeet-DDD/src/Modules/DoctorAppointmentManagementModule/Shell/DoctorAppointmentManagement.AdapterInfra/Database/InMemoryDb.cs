using DoctorAppointmentManagement.AdapterInfra.Entities;

namespace DoctorAppointmentManagement.AdapterInfra.Database;

public static class InMemoryDb
{
    public static List<AppointmentConfirmationEntity> AppointmentConfirmation = new();
}
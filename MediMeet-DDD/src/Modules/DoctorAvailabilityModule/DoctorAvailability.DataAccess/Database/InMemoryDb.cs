using DoctorAvailability.DataAccess.Entities;

namespace DoctorAvailability.DataAccess.Database;

public static class InMemoryDb
{
    public static List<Doctor> Doctors = new()
    {
        new Doctor
        {
            Id = 1,
            Name = "Dr. Mohamed Warda",
            Specialty = "Cardiology"
        },
        new Doctor
        {
            Id = 2,
            Name = "Dr. Sarah Smith",
            Specialty = "Pediatrics"
        },
        new Doctor
        {
            Id = 3,
            Name = "Dr. John Doe",
            Specialty = "General Medicine"
        }
    };

    public static List<Slot> Slots = new()
    {
        new Slot()
        {
            Id = new Guid("e4d7ff00-c256-4f25-b775-3c6819e3ff00"),
            Time = DateTime.Parse("2025-01-22T09:00:00"),
            DoctorId = 1,
            DoctorName = "Dr. Mohamed Warda",
            IsReserved = false,
            Cost = 50.0m
        },
        new Slot()
        {
            Id = new Guid("9e27e021-6134-46e6-b706-0f77ef5c19e1"),
            Time = DateTime.Parse("2025-01-22T10:00:00"),
            DoctorId = 1,
            DoctorName = "Dr. Mohamed Warda",
            IsReserved = false,
            Cost = 50.0m
        },
        new Slot()
        {
            Id = new Guid("067e7624-b61b-44c0-8581-50e083295b7f"),
            Time = DateTime.Parse("2025-01-22T11:00:00"),
            DoctorId = 1,
            DoctorName = "Dr. Mohamed Warda",
            IsReserved = true,
            Cost = 50.0m
        },
        new Slot()
        {
            Id = new Guid("23b18713-761e-4508-8462-c67fabb85607"),
            Time = DateTime.Parse("2025-01-22T12:00:00"),
            DoctorId = 1,
            DoctorName = "Dr. Mohamed Warda",
            IsReserved = false,
            Cost = 60.0m
        },
        // Additional slots for other doctors
        new Slot()
        {
            Id = new Guid("f5a62331-8dc2-4ce5-8a41-782b39f37f3d"),
            Time = DateTime.Parse("2025-01-22T09:00:00"),
            DoctorId = 2,
            DoctorName = "Dr. Sarah Smith",
            IsReserved = false,
            Cost = 45.0m
        },
        new Slot()
        {
            Id = new Guid("c3f2b45a-61a0-4d1b-8f56-88c25e4c09d0"),
            Time = DateTime.Parse("2025-01-22T10:00:00"),
            DoctorId = 2,
            DoctorName = "Dr. Sarah Smith",
            IsReserved = false,
            Cost = 45.0m
        }
    };
}
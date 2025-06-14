using Shared.RootEntity;

namespace DoctorAvailability.Abstractions.Models
{
    public class Doctor : AggregateRoot
    {
        public DoctorId Id { get; private set; }
        public string Name { get; private set; }
        public Specialty Specialty { get; private set; }
        public List<TimeSlot> TimeSlots { get; private set; } = new();

        private Doctor() { }

        public static Doctor Create(DoctorId id, string name, Specialty specialty)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Doctor name cannot be empty");

            return new Doctor
            {
                Id = id,
                Name = name,
                Specialty = specialty
            };
        }

        public void AddTimeSlot(TimeSlot slot)
        {
            if (TimeSlots.Any(s => s.Overlaps(slot)))
                throw new DomainException("Time slot overlaps with existing slot");

            TimeSlots.Add(slot);
        }

        public void RemoveTimeSlot(TimeSlot slot)
        {
            TimeSlots.Remove(slot);
        }
    }

    public record DoctorId
    {
        public Guid Value { get; }

        private DoctorId(Guid value) => Value = value;

        public static DoctorId New() => new(Guid.NewGuid());
        public static DoctorId From(Guid id) => new(id);
    }

    public enum Specialty
    {
        GeneralPractitioner,
        Pediatrician,
        Cardiologist,
        Dermatologist,
        Neurologist,
        Psychiatrist,
        Surgeon,
        Other
    }

    public class TimeSlot
    {
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public bool IsAvailable { get; private set; }

        private TimeSlot() { }

        public static TimeSlot Create(DateTime startTime, DateTime endTime)
        {
            if (startTime >= endTime)
                throw new DomainException("Start time must be before end time");

            if (startTime < DateTime.UtcNow)
                throw new DomainException("Cannot create time slots in the past");

            return new TimeSlot
            {
                StartTime = startTime,
                EndTime = endTime,
                IsAvailable = true
            };
        }

        public void MarkAsUnavailable()
        {
            IsAvailable = false;
        }

        public void MarkAsAvailable()
        {
            IsAvailable = true;
        }

        public bool Overlaps(TimeSlot other)
        {
            return StartTime < other.EndTime && EndTime > other.StartTime;
        }
    }
} 
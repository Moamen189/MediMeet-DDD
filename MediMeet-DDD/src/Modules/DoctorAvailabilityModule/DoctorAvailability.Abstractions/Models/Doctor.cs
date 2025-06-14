using Shared.RootEntity;
using DoctorAvailability.Business.DomainModels;

namespace DoctorAvailability.Abstractions.Models
{
    public class Doctor : AggregateRoot
    {
        private readonly List<TimeSlot> _availableSlots;
        
        private Doctor()
        {
            _availableSlots = new List<TimeSlot>();
        }

        private Doctor(DoctorId id, DoctorName name, Specialty specialty)
        {
            Id = id;
            Name = name;
            Specialty = specialty;
            _availableSlots = new List<TimeSlot>();
        }

        public DoctorId Id { get; private set; }
        public DoctorName Name { get; private set; }
        public Specialty Specialty { get; private set; }
        public IReadOnlyList<TimeSlot> AvailableSlots => _availableSlots.AsReadOnly();

        public static Doctor Create(int id, string name, string specialty)
        {
            return new Doctor(
                DoctorId.From(id),
                DoctorName.From(name),
                Specialty.From(specialty)
            );
        }

        public void AddTimeSlot(TimeSlot slot)
        {
            if (_availableSlots.Any(s => s.OverlapsWith(slot)))
                throw new DomainException("Time slot overlaps with existing slot");

            _availableSlots.Add(slot);
            AddDomainEvent(new AvailabilitySlotAddedDomainEvent(this, slot));
        }

        public void RemoveTimeSlot(TimeSlot slot)
        {
            if (!_availableSlots.Contains(slot))
                throw new DomainException("Time slot not found");

            if (slot.IsReserved)
                throw new DomainException("Cannot remove a reserved slot");

            _availableSlots.Remove(slot);
            AddDomainEvent(new AvailabilitySlotRemovedDomainEvent(this, slot));
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
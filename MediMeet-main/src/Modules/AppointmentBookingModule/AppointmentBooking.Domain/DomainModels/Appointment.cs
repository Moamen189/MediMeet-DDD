using System.ComponentModel;
using AppointmentBooking.Domain.DomainEvents;
using AppointmentBooking.Domain.Enums;
using Shared.RootEntity;

namespace AppointmentBooking.Domain.DomainModels;

public class Appointment : AggregateRoot
{
    private Appointment() { } // For EF Core

    private Appointment(
        Guid slotId,
        PatientId patientId,
        PatientName patientName,
        DoctorName doctorName,
        AppointmentDateTime reservedAt)
    {
        Id = AppointmentId.Create();
        SlotId = slotId;
        PatientId = patientId;
        PatientName = patientName;
        DoctorName = doctorName;
        ReservedAt = reservedAt;
        Status = AppointmentStatus.Pending;
        
        AddDomainEvent(new AppointmentCreatedDomainEvent(this));
    }

    public AppointmentId Id { get; private set; }
    public Guid SlotId { get; private set; }
    public PatientId PatientId { get; private set; }
    public PatientName PatientName { get; private set; }
    public DoctorName DoctorName { get; private set; }
    public AppointmentStatus Status { get; private set; }
    public AppointmentDateTime ReservedAt { get; private set; }

    public static Appointment Schedule(
        Guid slotId,
        PatientId patientId,
        PatientName patientName,
        DoctorName doctorName,
        AppointmentDateTime reservedAt)
    {
        if (reservedAt.Value < DateTime.UtcNow)
        {
            throw new DomainException("Cannot schedule appointment in the past");
        }

        return new Appointment(slotId, patientId, patientName, doctorName, reservedAt);
    }

    public void Cancel()
    {
        if (Status != AppointmentStatus.Pending)
        {
            throw new DomainException("Can only cancel pending appointments");
        }

        Status = AppointmentStatus.Cancelled;
        AddDomainEvent(new AppointmentCancelledDomainEvent(this));
    }

    public void Confirm()
    {
        if (Status != AppointmentStatus.Pending)
        {
            throw new DomainException("Can only confirm pending appointments");
        }

        Status = AppointmentStatus.Confirmed;
        AddDomainEvent(new AppointmentConfirmedDomainEvent(this));
    }
}

public enum AppointmentStatus
{
    Pending,    
    Confirmed,
    Cancelled
}
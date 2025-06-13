using AppointmentBooking.Domain.DomainModels;
using MediatR;

namespace AppointmentBooking.Application.CreateAppointment;

public record CreateAppointmentCommand : IRequest<AppointmentId>
{
    public Guid SlotId { get; init; }
    public int PatientId { get; init; }
    public string PatientName { get; init; }
    public string DoctorName { get; init; }
    public DateTime ReservedAt { get; init; }
} 
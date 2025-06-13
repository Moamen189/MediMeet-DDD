using AppointmentBooking.Domain.DomainModels;
using AppointmentBooking.Domain.IRepository;
using MediatR;

namespace AppointmentBooking.Application.CreateAppointment;

public class CreateAppointmentHandler : IRequestHandler<CreateAppointmentCommand, AppointmentId>
{
    private readonly IAppointmentRepository _appointmentRepository;
    
    public CreateAppointmentHandler(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }
   
    public async Task<AppointmentId> Handle(CreateAppointmentCommand command, CancellationToken cancellationToken)
    {
        var appointment = Appointment.Schedule(
            command.SlotId,
            PatientId.From(command.PatientId),
            PatientName.From(command.PatientName),
            DoctorName.From(command.DoctorName),
            AppointmentDateTime.From(command.ReservedAt)
        );

        var created = await _appointmentRepository.CreateAsync(appointment);
        return created.Id;
    }
}
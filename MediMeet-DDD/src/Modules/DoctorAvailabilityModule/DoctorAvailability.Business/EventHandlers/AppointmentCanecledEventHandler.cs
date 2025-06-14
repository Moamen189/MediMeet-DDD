using DoctorAppointmentManagement.Shared.IntegrationEvents;
using DoctorAvailability.Abstractions.Models;
using DoctorAvailability.Abstractions.Repositories;
using MediatR;

namespace DoctorAvailability.Business.EventHandlers
{
    public class AppointmentCanceledEventHandler : INotificationHandler<AppointmentCanceledEvent>
    {
        private readonly IDoctorRepository _doctorRepository;

        public AppointmentCanceledEventHandler(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task Handle(AppointmentCanceledEvent notification, CancellationToken cancellationToken)
        {
            var doctor = await _doctorRepository.GetByIdAsync(DoctorId.From(notification.DoctorId));
            var slot = doctor.TimeSlots.FirstOrDefault(s => s.StartTime == notification.AppointmentTime);
            
            if (slot != null)
            {
                slot.MarkAsAvailable();
                await _doctorRepository.UpdateAsync(doctor);
            }
        }
    }
}
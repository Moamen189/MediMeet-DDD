using DoctorAppointmentManagement.Core.Domain.Models;
using DoctorAppointmentManagement.Core.Domain.Enums;

namespace DoctorAppointmentManagement.Core.Domain.Repositories;

public interface IAppointmentConfirmationRepository
{
    Task<AppointmentConfirmation> GetByIdAsync(AppointmentConfirmationId id);
    Task<AppointmentConfirmation> GetByAppointmentIdAsync(AppointmentId appointmentId);
    Task<AppointmentConfirmation> AddAsync(AppointmentConfirmation appointment);
    Task UpdateAsync(AppointmentConfirmation appointment);
    Task<List<AppointmentConfirmation>> GetUpcomingAppointmentsAsync();
    Task<List<AppointmentConfirmation>> GetAppointmentsByPatientAsync(int patientId);
    Task<List<AppointmentConfirmation>> GetAppointmentsByStatusAsync(AppointmentStatus status);
} 
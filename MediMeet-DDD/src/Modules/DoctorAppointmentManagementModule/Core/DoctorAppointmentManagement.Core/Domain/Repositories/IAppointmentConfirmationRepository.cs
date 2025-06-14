using DoctorAppointmentManagement.Core.Domain.Models;
using DoctorAppointmentManagement.Core.Domain.Enums;

namespace DoctorAppointmentManagement.Core.Domain.Repositories;

public interface IAppointmentConfirmationRepository
{
    Task<AppointmentConfirmation> GetByIdAsync(AppointmentConfirmationId id);
    Task<AppointmentConfirmation> GetByAppointmentIdAsync(AppointmentId appointmentId);
    Task<List<AppointmentConfirmation>> GetByPatientIdAsync(int patientId);
    Task<List<AppointmentConfirmation>> GetByDoctorIdAsync(string doctorId);
    Task<List<AppointmentConfirmation>> GetByStatusAsync(Models.AppointmentStatus status);
    Task<AppointmentConfirmation> AddAsync(AppointmentConfirmation appointment);
    Task<AppointmentConfirmation> UpdateAsync(AppointmentConfirmation appointment);
} 
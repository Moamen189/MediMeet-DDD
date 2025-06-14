using DoctorAppointmentManagement.Core.Domain.Models;
using DoctorAppointmentManagement.Core.Domain.Enums;

namespace DoctorAppointmentManagement.Core.Domain.Repositories;

public interface IAppointmentConfirmationRepository
{
    Task<AppointmentConfirmation> GetByIdAsync(string id);
    Task<List<AppointmentConfirmation>> GetByPatientIdAsync(string patientId);
    Task<List<AppointmentConfirmation>> GetByDoctorIdAsync(string doctorId);
    Task<List<AppointmentConfirmation>> GetByStatusAsync(Models.AppointmentStatus status);
    Task AddAsync(AppointmentConfirmation appointment);
    Task UpdateAsync(AppointmentConfirmation appointment);
} 
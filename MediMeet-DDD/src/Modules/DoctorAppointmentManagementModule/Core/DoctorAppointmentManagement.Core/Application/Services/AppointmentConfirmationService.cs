using DoctorAppointmentManagement.Core.Domain.Models;
using DoctorAppointmentManagement.Core.Domain.Repositories;
using DoctorAppointmentManagement.Core.Application.Dtos;

namespace DoctorAppointmentManagement.Core.Application.Services;

public class AppointmentConfirmationService
{
    private readonly IAppointmentConfirmationRepository _repository;

    public AppointmentConfirmationService(IAppointmentConfirmationRepository repository)
    {
        _repository = repository;
    }

    public async Task<AppointmentConfirmationDto> CreateAppointmentConfirmationAsync(
        CreateAppointmentConfirmationDto dto)
    {
        var appointment = AppointmentConfirmation.Create(
            dto.AppointmentId,
            dto.SlotId,
            dto.PatientId,
            dto.PatientName,
            dto.ReservedAt,
            dto.Comments
        );

        await _repository.AddAsync(appointment);
        return MapToDto(appointment);
    }

    public async Task<AppointmentConfirmationDto> ConfirmAppointmentAsync(
        Guid appointmentId,
        string? comments = null)
    {
        var appointment = await _repository.GetByAppointmentIdAsync(AppointmentId.From(appointmentId));
        appointment.Confirm(comments);
        await _repository.UpdateAsync(appointment);
        return MapToDto(appointment);
    }

    public async Task<AppointmentConfirmationDto> CancelAppointmentAsync(
        Guid appointmentId,
        string reason)
    {
        var appointment = await _repository.GetByAppointmentIdAsync(AppointmentId.From(appointmentId));
        appointment.Cancel(reason);
        await _repository.UpdateAsync(appointment);
        return MapToDto(appointment);
    }

    public async Task<List<AppointmentConfirmationDto>> GetUpcomingAppointmentsAsync()
    {
        var appointments = await _repository.GetUpcomingAppointmentsAsync();
        return appointments.Select(MapToDto).ToList();
    }

    public async Task<List<AppointmentConfirmationDto>> GetAppointmentsByPatientAsync(int patientId)
    {
        var appointments = await _repository.GetAppointmentsByPatientAsync(patientId);
        return appointments.Select(MapToDto).ToList();
    }

    private static AppointmentConfirmationDto MapToDto(AppointmentConfirmation appointment)
    {
        return new AppointmentConfirmationDto
        {
            Id = appointment.Id.Value,
            AppointmentId = appointment.AppointmentId.Value,
            SlotId = appointment.SlotId.Value,
            PatientId = appointment.PatientInfo.Id,
            PatientName = appointment.PatientInfo.Name,
            Status = appointment.Status,
            Comments = appointment.Comments,
            ReservedAt = appointment.ReservedAt.Value,
            UpdatedAt = appointment.UpdatedAt
        };
    }
} 
using DoctorAppointmentManagement.Core.Application.Dtos;
using DoctorAppointmentManagement.Core.Domain.Models;
using DoctorAppointmentManagement.Core.Domain.Repositories;
using Shared.RootEntity;

namespace DoctorAppointmentManagement.Core.Application.Services;

public class AppointmentConfirmationService
{
    private readonly IAppointmentConfirmationRepository _repository;

    public AppointmentConfirmationService(IAppointmentConfirmationRepository repository)
    {
        _repository = repository;
    }

    public async Task<AppointmentConfirmationDto> CreateAppointmentConfirmationAsync(
        CreateAppointmentConfirmationDto request)
    {
        var appointmentConfirmation = AppointmentConfirmation.Create(
            AppointmentId.From(request.AppointmentId),
            request.Comments);

        await _repository.AddAsync(appointmentConfirmation);

        return MapToDto(appointmentConfirmation);
    }

    public async Task<AppointmentConfirmationDto> ConfirmAppointmentAsync(
        Guid appointmentId,
        string comments)
    {
        var appointmentConfirmation = await _repository.GetByAppointmentIdAsync(
            AppointmentId.From(appointmentId));

        appointmentConfirmation.Confirm(comments);
        await _repository.UpdateAsync(appointmentConfirmation);

        return MapToDto(appointmentConfirmation);
    }

    public async Task<AppointmentConfirmationDto> CancelAppointmentAsync(
        Guid appointmentId,
        string reason)
    {
        var appointmentConfirmation = await _repository.GetByAppointmentIdAsync(
            AppointmentId.From(appointmentId));

        appointmentConfirmation.Cancel(reason);
        await _repository.UpdateAsync(appointmentConfirmation);

        return MapToDto(appointmentConfirmation);
    }

    public async Task<List<AppointmentConfirmationDto>> GetUpcomingAppointmentsAsync()
    {
        var appointments = await _repository.GetUpcomingAppointmentsAsync();
        return appointments.Select(MapToDto).ToList();
    }

    public async Task<List<AppointmentConfirmationDto>> GetAppointmentsByPatientAsync(int patientId)
    {
        var appointments = await _repository.GetByPatientIdAsync(patientId);
        return appointments.Select(MapToDto).ToList();
    }

    private static AppointmentConfirmationDto MapToDto(AppointmentConfirmation appointment)
    {
        return new AppointmentConfirmationDto
        {
            Id = appointment.Id.Value,
            AppointmentId = appointment.AppointmentId.Value,
            Status = appointment.Status,
            Comments = appointment.Comments,
            CreatedAt = appointment.CreatedAt,
            LastModifiedAt = appointment.LastModifiedAt
        };
    }
} 
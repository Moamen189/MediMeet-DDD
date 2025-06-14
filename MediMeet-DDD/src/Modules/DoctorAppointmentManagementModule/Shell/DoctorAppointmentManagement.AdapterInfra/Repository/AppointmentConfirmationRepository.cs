using DoctorAppointmentManagement.AdapterInfra.Database;
using DoctorAppointmentManagement.AdapterInfra.Entities;
using DoctorAppointmentManagement.Core.Domain.Enums;
using DoctorAppointmentManagement.Core.Domain.Models;
using DoctorAppointmentManagement.Core.Domain.Repositories;
using MediatR;

namespace DoctorAppointmentManagement.AdapterInfra.Repository;

public class AppointmentConfirmationRepository : IAppointmentConfirmationRepository
{
    private readonly IMediator _mediator;

    public AppointmentConfirmationRepository(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task<AppointmentConfirmation> GetByIdAsync(AppointmentConfirmationId id)
    {
        var entity = InMemoryDb.AppointmentConfirmations.FirstOrDefault(a => a.Id == id.Value);
        if (entity == null)
        {
            throw new DomainException($"Appointment confirmation with ID {id.Value} not found");
        }

        return Task.FromResult(MapToDomain(entity));
    }

    public Task<AppointmentConfirmation> GetByAppointmentIdAsync(AppointmentId appointmentId)
    {
        var entity = InMemoryDb.AppointmentConfirmations.FirstOrDefault(a => a.AppointmentId == appointmentId.Value);
        if (entity == null)
        {
            throw new DomainException($"Appointment confirmation for appointment {appointmentId.Value} not found");
        }

        return Task.FromResult(MapToDomain(entity));
    }

    public async Task<AppointmentConfirmation> AddAsync(AppointmentConfirmation appointment)
    {
        var entity = MapToEntity(appointment);
        InMemoryDb.AppointmentConfirmations.Add(entity);

        foreach (var domainEvent in appointment.DomainEvents)
        {
            await _mediator.Publish(domainEvent);
        }

        return appointment;
    }

    public async Task<AppointmentConfirmation> UpdateAsync(AppointmentConfirmation appointment)
    {
        var entity = InMemoryDb.AppointmentConfirmations.FirstOrDefault(a => a.Id == appointment.Id.Value);
        if (entity == null)
        {
            throw new DomainException($"Appointment confirmation with ID {appointment.Id.Value} not found");
        }

        // Update entity properties
        var updatedEntity = MapToEntity(appointment);
        entity.AppointmentId = updatedEntity.AppointmentId;
        entity.Status = updatedEntity.Status;
        entity.Comments = updatedEntity.Comments;
        entity.LastModifiedAt = DateTime.UtcNow;

        foreach (var domainEvent in appointment.DomainEvents)
        {
            await _mediator.Publish(domainEvent);
        }

        return appointment;
    }

    public Task<List<AppointmentConfirmation>> GetUpcomingAppointmentsAsync()
    {
        var entities = InMemoryDb.AppointmentConfirmations
            .Where(a => a.ReservedAt > DateTime.UtcNow)
            .OrderBy(a => a.ReservedAt)
            .Select(MapToDomain)
            .ToList();

        return Task.FromResult(entities);
    }

    public Task<List<AppointmentConfirmation>> GetAppointmentsByPatientAsync(int patientId)
    {
        var entities = InMemoryDb.AppointmentConfirmations
            .Where(a => a.PatientId == patientId)
            .OrderByDescending(a => a.ReservedAt)
            .Select(MapToDomain)
            .ToList();

        return Task.FromResult(entities);
    }

    public Task<List<AppointmentConfirmation>> GetAppointmentsByStatusAsync(AppointmentStatus status)
    {
        var entities = InMemoryDb.AppointmentConfirmations
            .Where(a => a.Status == status)
            .OrderByDescending(a => a.ReservedAt)
            .Select(MapToDomain)
            .ToList();

        return Task.FromResult(entities);
    }

    private AppointmentConfirmation MapToDomain(AppointmentConfirmationEntity entity)
    {
        return AppointmentConfirmation.Reconstitute(
            AppointmentConfirmationId.From(entity.Id),
            AppointmentId.From(entity.AppointmentId),
            (AppointmentStatus)entity.Status,
            entity.Comments
        );
    }

    private AppointmentConfirmationEntity MapToEntity(AppointmentConfirmation domain)
    {
        return new AppointmentConfirmationEntity
        {
            Id = domain.Id.Value,
            AppointmentId = domain.AppointmentId.Value,
            Status = (int)domain.Status,
            Comments = domain.Comments,
            CreatedAt = DateTime.UtcNow,
            LastModifiedAt = DateTime.UtcNow
        };
    }
} 
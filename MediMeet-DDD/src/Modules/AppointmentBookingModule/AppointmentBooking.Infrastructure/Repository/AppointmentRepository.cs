using AppointmentBooking.Domain.DomainModels;
using AppointmentBooking.Domain.IRepository;
using AppointmentBooking.Infrastructure.Database;
using AppointmentBooking.Infrastructure.Entities;
using AppointmentBooking.Infrastructure.Extensions;
using MediatR;

namespace AppointmentBooking.Infrastructure.Repository;

public class AppointmentRepository : IAppointmentRepository
{
	private readonly IMediator _mediator;

	public AppointmentRepository(IMediator mediator)
	{
		_mediator = mediator;
	}

	public async Task<Appointment> CreateAsync(Appointment appointment)
	{
		var entity = appointment.ToEntity();
		InMemoryDb.Appointments.Add(entity);
		
		foreach (var domainEvent in appointment.DomainEvents)
		{
			await _mediator.Publish(domainEvent);
		}
		
		return appointment;
	}

	public Task<List<Appointment>> GetUpcomingAppointmentsAsync()
	{
		var appointments = InMemoryDb.Appointments
			.Where(x => x.Status == Domain.Enums.AppointmentStatus.Pending)
			.Select(x => x.ToDomainModel())
			.ToList();
			
		return Task.FromResult(appointments);
	}

	public Task<Appointment> GetByIdAsync(AppointmentId id)
	{
		var entity = InMemoryDb.Appointments.FirstOrDefault(x => x.Id == id.Value);
		if (entity == null)
		{
			throw new DomainException($"Appointment with ID {id.Value} not found");
		}
		
		return Task.FromResult(entity.ToDomainModel());
	}

	public Task<bool> UpdateAsync(Appointment appointment)
	{
		var entity = InMemoryDb.Appointments.FirstOrDefault(x => x.Id == appointment.Id.Value);
		if (entity == null)
		{
			return Task.FromResult(false);
		}

		entity.Status = appointment.Status;
		
		foreach (var domainEvent in appointment.DomainEvents)
		{
			_mediator.Publish(domainEvent);
		}
		
		return Task.FromResult(true);
	}
}
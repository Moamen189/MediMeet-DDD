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

	public async Task<bool> UpdateAsync(Appointment appointment)
	{
		var entity = InMemoryDb.Appointments.FirstOrDefault(x => x.Id == appointment.Id.Value);
		if (entity == null)
		{
			return false;
		}

		// Update all properties
		var updatedEntity = appointment.ToEntity();
		entity.Status = updatedEntity.Status;
		entity.ReservedAt = updatedEntity.ReservedAt;
		
		foreach (var domainEvent in appointment.DomainEvents)
		{
			await _mediator.Publish(domainEvent);
		}
		
		return true;
	}

	public Task<List<Appointment>> GetPatientAppointmentsAsync(PatientId patientId)
	{
		var appointments = InMemoryDb.Appointments
			.Where(x => x.PatientId == patientId.Value)
			.Select(x => x.ToDomainModel())
			.ToList();
			
		return Task.FromResult(appointments);
	}

	public Task<bool> ExistsOverlappingAppointmentAsync(PatientId patientId, AppointmentDateTime dateTime)
	{
		// Check for any appointments within 1 hour before or after the given time
		var oneHourBefore = dateTime.Value.AddHours(-1);
		var oneHourAfter = dateTime.Value.AddHours(1);
		
		var exists = InMemoryDb.Appointments.Any(x => 
			x.PatientId == patientId.Value &&
			x.Status != Domain.Enums.AppointmentStatus.Cancelled &&
			x.ReservedAt >= oneHourBefore &&
			x.ReservedAt <= oneHourAfter);
			
		return Task.FromResult(exists);
	}

	public Task<int> GetPatientAppointmentCountInPeriodAsync(PatientId patientId, DateTime startDate, DateTime endDate)
	{
		var count = InMemoryDb.Appointments.Count(x =>
			x.PatientId == patientId.Value &&
			x.Status != Domain.Enums.AppointmentStatus.Cancelled &&
			x.ReservedAt >= startDate &&
			x.ReservedAt <= endDate);
			
		return Task.FromResult(count);
	}

	public Task<List<Appointment>> GetAppointmentsForDoctorAsync(DoctorName doctorName, DateTime date)
	{
		var appointments = InMemoryDb.Appointments
			.Where(x => 
				x.DoctorName == doctorName.Value &&
				x.ReservedAt.Date == date.Date &&
				x.Status != Domain.Enums.AppointmentStatus.Cancelled)
			.Select(x => x.ToDomainModel())
			.ToList();
			
		return Task.FromResult(appointments);
	}
}
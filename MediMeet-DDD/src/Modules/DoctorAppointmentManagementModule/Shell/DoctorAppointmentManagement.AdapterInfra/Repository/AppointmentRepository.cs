using AppointmentBooking.Shared.Contracts;
using DoctorAppointmentManagement.AdapterInfra.Database;
using DoctorAppointmentManagement.AdapterInfra.Extension;
using DoctorAppointmentManagement.Core.Domain.Enums;
using DoctorAppointmentManagement.Core.Dtos;
using DoctorAppointmentManagement.Core.Models;
using DoctorAppointmentManagement.Core.OutputPorts.IRepository;
using DoctorAppointmentManagement.Shared.IntegrationEvents;
using MediatR;

namespace DoctorAppointmentManagement.AdapterInfra.Repository
{
	public class AppointmentRepository : IAppointmentRepository
	{
		private readonly IAppointmentBookingApi _appointmentBookingApi;
		private readonly IMediator _mediator;

		public AppointmentRepository(IAppointmentBookingApi appointmentBookingApi,IMediator mediator)
		{
			_appointmentBookingApi = appointmentBookingApi;
			_mediator = mediator;
		}



		public async Task<List<AppointmentConfirmationDto>> GetUpComingAppointments()
		{
			var entity = await _appointmentBookingApi.GetUpComingAppointments();
			var models = entity.Select(a => new AppointmentConfirmationDto()
			{
				AppointmentId = a.Id,
				PatientId = a.PatientId,
				PatientName = a.PatientName,
				ReservedAt = a.ReservedAt,
				SlotId = a.SlotId,
			}).ToList();
			return models;
		}

		public async Task<AppointmentConfirmation> CompleteUpComingAppointment(AppointmentConfirmationDto appointmentConfirmationDto)
		{
			if (appointmentConfirmationDto == null)
				throw new ArgumentNullException(nameof(appointmentConfirmationDto));

			var AppointmentConfirmation = new AppointmentConfirmation
			{
				Id = Guid.NewGuid(),
				UpdatedAt = DateTime.Now,
				Comments = appointmentConfirmationDto.Comments,
				AppointmentId = appointmentConfirmationDto.AppointmentId,
				SlotId = appointmentConfirmationDto.SlotId,
				PatientId = appointmentConfirmationDto.PatientId,
				PatientName = appointmentConfirmationDto.PatientName,
				AppointmentStatus = AppointmentStatus.Confirmed,
				ReservedAt = appointmentConfirmationDto.ReservedAt
			};
			var entity = AppointmentConfirmation.ToEntity();
			InMemoryDb.AppointmentConfirmation.Add(entity);
			await _mediator.Publish(new  AppointmentConfirmedEvent(appointmentConfirmationDto.AppointmentId));
			return entity.ToModel();

		}

		public async Task<AppointmentConfirmation> CancelUpComingAppointment(AppointmentConfirmationDto appointmentConfirmationDto)
		{
			if (appointmentConfirmationDto == null)
				throw new ArgumentNullException(nameof(appointmentConfirmationDto));

			var AppointmentConfirmation = new AppointmentConfirmation
			{
				Id = Guid.NewGuid(),
				UpdatedAt = DateTime.Now,
				Comments = appointmentConfirmationDto.Comments,
				AppointmentId = appointmentConfirmationDto.AppointmentId,
				SlotId = appointmentConfirmationDto.SlotId,
				PatientId = appointmentConfirmationDto.PatientId,
				PatientName = appointmentConfirmationDto.PatientName,
				AppointmentStatus = AppointmentStatus.Cancelled,
				ReservedAt = appointmentConfirmationDto.ReservedAt
			};
			var entity = AppointmentConfirmation.ToEntity();
			InMemoryDb.AppointmentConfirmation.Add(entity);
			 await _mediator.Publish(new  AppointmentCanceledEvent(appointmentConfirmationDto.AppointmentId,appointmentConfirmationDto.SlotId));
			return entity.ToModel();
		}
	}
}

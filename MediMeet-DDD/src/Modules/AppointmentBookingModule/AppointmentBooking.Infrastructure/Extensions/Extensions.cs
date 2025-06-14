using AppointmentBooking.Domain.DomainModels;
using AppointmentBooking.Infrastructure.Entities;
using AppointmentBooking.Domain.IRepository;
using AppointmentBooking.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace AppointmentBooking.Infrastructure.Extensions;

public static class Extensions
{
    public static AppointmentEntity ToEntity(this Appointment appointment)
    {
        return new AppointmentEntity
        {
            Id = appointment.Id.Value,
            SlotId = appointment.SlotId,
            PatientId = appointment.PatientId.Value,
            PatientName = appointment.PatientName.Value,
            DoctorName = appointment.DoctorName.Value,
            Status = appointment.Status,
            ReservedAt = appointment.ReservedAt.Value
        };
    }

    public static Appointment ToDomainModel(this AppointmentEntity entity)
    {
        return Appointment.Schedule(
            entity.SlotId,
            PatientId.From(entity.PatientId),
            PatientName.From(entity.PatientName),
            DoctorName.From(entity.DoctorName),
            AppointmentDateTime.From(entity.ReservedAt)
        );
    }

    public static IServiceCollection AddAppointmentBookingInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IPatientRepository, PatientRepository>();
        
        return services;
    }
} 
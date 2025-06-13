using AppointmentConfirmation.Api.Infrastructure.Entities;

namespace AppointmentConfirmation.Api.Infrastructure.Database;

public static class InMemoryDb
{
    public static List<NotificationEntity> Notifications = new();
} 
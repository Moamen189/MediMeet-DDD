using AppointmentConfirmation.Api.Domain.Models;

namespace AppointmentConfirmation.Api.Infrastructure.Entities;

public class NotificationEntity
{
    public Guid Id { get; set; }
    public string Type { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public string RecipientEmail { get; set; }
    public string RecipientName { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? SentAt { get; set; }
} 
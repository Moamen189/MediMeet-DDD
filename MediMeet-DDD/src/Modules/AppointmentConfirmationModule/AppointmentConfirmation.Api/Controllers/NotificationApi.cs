using AppointmentConfirmation.Shared;
using Microsoft.Extensions.Logging;

namespace AppointmentConfirmation.Api.Controllers
{
	public class NotificationApi : INotificationApi
	{
		private readonly ILogger<NotificationApi> logger;

		public NotificationApi(ILogger<NotificationApi> logger)
		{
			this.logger = logger;
		}

		public void SendNotification(List<string> receivers, string message)
		{
			foreach (var receiver in receivers)
			{
				var notification = $"""
					==============================
					
					Receiver: {receiver}

					Message:
					{message}
					
					==============================
					""";
				logger.LogInformation(notification);
			}
		}
	}
}

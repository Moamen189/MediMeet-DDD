namespace AppointmentConfirmation.Shared
{
	public interface INotificationApi
	{
		public void SendNotification(List<string> recivers, string message);
	}
}

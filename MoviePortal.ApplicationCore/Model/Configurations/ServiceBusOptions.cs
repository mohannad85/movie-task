namespace MoviePortal.ApplicationCore.Model.Configurations
{
	public class ServiceBusOptions
	{
		public string? ServiceBusConnectionString { get; set; }
		public string? QueueName { get; set; }
	}
}

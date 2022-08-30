using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using MoviePortal.ApplicationCore.Interfaces.Service;
using MoviePortal.ApplicationCore.Model;
using System.Text.Json;

namespace MoviePortal.ApplicationCore.Service
{
	public class MessagePublisher : IMessagePublisher
	{
		private readonly ServiceBusClient _client;
		private readonly ILogger<MessagePublisher> _logger;

		public MessagePublisher(ServiceBusClient client, ILogger<MessagePublisher> logger)
		{
			_client = client ?? throw new ArgumentNullException(nameof(client));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task Publish()
		{
			var sender = _client.CreateSender("**");
			var message = JsonSerializer.Serialize(new MessageRequest() { Id = Guid.NewGuid(), Created = DateTime.Now });
			var @event = new ServiceBusMessage(message);
			await sender.SendMessageAsync(@event);
		}
	}
}

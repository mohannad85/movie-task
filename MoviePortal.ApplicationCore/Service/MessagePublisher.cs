using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MoviePortal.ApplicationCore.Interfaces.Service;
using MoviePortal.ApplicationCore.Model;
using System.Text.Json;

namespace MoviePortal.ApplicationCore.Service
{
	public class MessagePublisher : IMessagePublisher
	{
		private readonly ServiceBusClient _client;
		private readonly IConfiguration _configuration;
		private readonly ILogger<MessagePublisher> _logger;

		public MessagePublisher(ServiceBusClient client, IConfiguration configuration, ILogger<MessagePublisher> logger)
		{
			_client = client ?? throw new ArgumentNullException(nameof(client));
			_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task Publish()
		{
			// TODO: bind appsetting and ServiceBusOptions object 
			var sender = _client.CreateSender(_configuration["ServiceBusOptions:QueueName"]);
			var message = JsonSerializer.Serialize(new MessageRequest() { Id = Guid.NewGuid(), Created = DateTime.Now });
			var @event = new ServiceBusMessage(message);
			await sender.SendMessageAsync(@event);
		}
	}
}

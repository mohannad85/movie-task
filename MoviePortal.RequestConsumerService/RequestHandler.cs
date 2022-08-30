using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Azure;

namespace MoviePortal.RequestConsumerService
{
	public class RequestHandler : BackgroundService
	{
		private readonly HttpClient _httpClient;
		private readonly ServiceBusClient _client;
		private ServiceBusProcessor _processor;
		private readonly ILogger<RequestHandler> _logger;

		public RequestHandler(HttpClient httpClient, IAzureClientFactory<ServiceBusClient> serviceBusClientFactory, ILogger<RequestHandler> logger)
		{
			_httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
			_client = serviceBusClientFactory.CreateClient("ClientService");
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			_processor = _client.CreateProcessor("**", new ServiceBusProcessorOptions());
			_processor.ProcessMessageAsync += MessageHandler;
			_processor.ProcessErrorAsync += ErrorHandler;

			await _processor.StartProcessingAsync();
			Console.WriteLine($"StartProcessingAsync");

			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri("**"),
				Headers =
					{
						{ "X-RapidAPI-Key", "**" },
						{ "X-RapidAPI-Host", "movies-app1.p.rapidapi.com" },
					},
			};

			var response = await _httpClient.SendAsync(request);
			response.EnsureSuccessStatusCode();
			var body = await response.Content.ReadAsStringAsync();
			Console.WriteLine(body);
		}

		private async Task MessageHandler(ProcessMessageEventArgs args)
		{
			string body = args.Message.Body.ToString();
			Console.WriteLine($"Received: {body}");
			await args.CompleteMessageAsync(args.Message);
		}

		private Task ErrorHandler(ProcessErrorEventArgs args)
		{
			Console.WriteLine(args.Exception.ToString());
			return Task.CompletedTask;
		}

		public override async Task StopAsync(CancellationToken cancellationToken)
		{
			await _processor.StopProcessingAsync();
			await base.StopAsync(cancellationToken);
		}

		public override void Dispose()
		{
			_processor.DisposeAsync().GetAwaiter().GetResult();
			_client.DisposeAsync().GetAwaiter().GetResult();
		}
	}
}
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Azure;
using MoviePortal.Common.AzureStorageServices.Interfaces;
using MoviePortal.Common.Movie.DTO;
using MoviePortal.Common.Movie.Model;
using MoviePortal.Common.Responses;
using System.Text.Json;

namespace MoviePortal.RequestConsumerService
{
	public class RequestHandler : BackgroundService
	{
		private readonly HttpClient _httpClient;
		private readonly ServiceBusClient _client;
		private ServiceBusProcessor _processor;
		private readonly IConfiguration _configuration;
		private readonly ITableStorageService _tableStorageService;
		private readonly ILogger<RequestHandler> _logger;

		public RequestHandler(
			HttpClient httpClient,
			IAzureClientFactory<ServiceBusClient> serviceBusClientFactory, 
			IConfiguration configuration,
			ITableStorageService tableStorageService,
			ILogger<RequestHandler> logger)
		{
			_httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
			_client = serviceBusClientFactory.CreateClient("ClientService");
			_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
			_tableStorageService = tableStorageService ?? throw new ArgumentNullException(nameof(tableStorageService));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			_processor = _client.CreateProcessor(_configuration["ServiceBusOptions:QueueName"], new ServiceBusProcessorOptions());
			_processor.ProcessMessageAsync += MessageHandler;
			_processor.ProcessErrorAsync += ErrorHandler;

			await _processor.StartProcessingAsync();
		}

		private async Task MessageHandler(ProcessMessageEventArgs args)
		{
			string body = args.Message.Body.ToString();
			Console.WriteLine($"Received: {body}");
			_logger.LogInformation($"@event received {body}");
			await args.CompleteMessageAsync(args.Message);
			await RequestProcessor();
		}

		private Task ErrorHandler(ProcessErrorEventArgs args)
		{
			Console.WriteLine(args.Exception.ToString());
			_logger.LogError($"Throwen Exception in ErrorHandler: {args.Exception.ToString()}");
			return Task.CompletedTask;
		}

		private async Task RequestProcessor()
		{
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri(_configuration["RapidOptions:RapidBaseUrl"]),
				Headers =
				{
					{ "X-RapidAPI-Key", _configuration["RapidOptions:RapidKey"] },
					{ "X-RapidAPI-Host", _configuration["RapidOptions:RapidHost"] },
				}
			};

			var response = await _httpClient.SendAsync(request);
			response.EnsureSuccessStatusCode();
			var body = await response.Content.ReadAsStringAsync();
			var apiResponse = JsonSerializer.Deserialize<ApiResponse>(body);
			var movieBasicInfo = new List<MovieBasicInfoDTO>();
			if (apiResponse?.Results != null && apiResponse.Results.Any())
			{
				foreach (var movie in apiResponse?.Results)
				{
					movieBasicInfo.Add(
						new MovieBasicInfoDTO()
						{
							Image = movie?.Image ?? "",
							TitleOriginal = movie?.TitleOriginal ?? "",
							Rating = movie?.Rating ?? ""
						}
						);
				}
				var entity = new MovieEntity()
				{
					Content = JsonSerializer.Serialize(movieBasicInfo)
				};
				await _tableStorageService.UpsertEntityAsync(entity);
			}
			else
				_logger.LogError("Error in receiving response");
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
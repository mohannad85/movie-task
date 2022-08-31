using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using MoviePortal.ApplicationCore.Interfaces.Service;
using MoviePortal.Common.AzureStorageServices.Interfaces;
using MoviePortal.Common.Movie.DTO;
using System.Text.Json;

namespace MoviePortal.ApplicationCore.Service
{
	public class MovieService : IMovieService
	{
		private readonly ITableStorageService _tableStorageService;
		private readonly ILogger<MovieService> _logger;
		
		public MovieService(ServiceBusClient client, ITableStorageService tableStorageService, ILogger<MovieService> logger)
		{
			_tableStorageService = tableStorageService ?? throw new ArgumentNullException(nameof(tableStorageService));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task<List<MovieBasicInfoDTO>> GetAllAsync()
		{
			var response = await _tableStorageService.GetEntityAsync();
			return JsonSerializer.Deserialize<List<MovieBasicInfoDTO>>(response?.Content);
		}
	}
}

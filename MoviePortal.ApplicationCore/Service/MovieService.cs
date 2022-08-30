using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using MoviePortal.ApplicationCore.Interfaces.Service;
using MoviePortal.ApplicationCore.Model;
using MoviePortal.ApplicationCore.Model.DTO;
using System.Text.Json;

namespace MoviePortal.ApplicationCore.Service
{
	public class MovieService : IMovieService
	{
		
		private readonly ILogger<MovieService> _logger;
		
		public MovieService(ServiceBusClient client, ILogger<MovieService> logger)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task<List<MovieDTO>> GetAllAsync()
		{
			return new List<MovieDTO>();
		}
	}
}

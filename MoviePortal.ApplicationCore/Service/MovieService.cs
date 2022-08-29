using Microsoft.Extensions.Logging;
using MoviePortal.ApplicationCore.Interfaces.Service;
using MoviePortal.ApplicationCore.Model.DTO;

namespace MoviePortal.ApplicationCore.Service
{
	public class MovieService : IMovieService
	{
		private readonly ILogger<MovieService> _logger;
		
		public MovieService(ILogger<MovieService> logger)
		{
			_logger = logger;
		}

		public async Task<List<MovieDTO>> GetAllAsync()
		{
			return new List<MovieDTO>();
		}
	}
}

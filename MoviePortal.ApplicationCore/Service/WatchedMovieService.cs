using Microsoft.Extensions.Logging;
using MoviePortal.ApplicationCore.Interfaces.Infrastructure;
using MoviePortal.ApplicationCore.Interfaces.Service;
using MoviePortal.ApplicationCore.Model.DTO;

namespace MoviePortal.ApplicationCore.Service
{
	public class WatchedMovieService : IWatchedMovieService
	{
		private readonly ILogger<WatchedMovieService> _logger;

		public WatchedMovieService(ILogger<WatchedMovieService> logger)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task<WatchedMovieDTO> CreateWatchedMovieAsync(WatchedMovieDTO dto)
		{
			if (dto == null)
			{
				_logger.LogWarning($"Dto object is null {dto}");
				throw new ArgumentNullException(nameof(dto));
			}

			var watchedMovie = WatchedMovieDTO.Deconstruct(dto);
			watchedMovie.CreatedDate = DateTime.Now;
			watchedMovie.CreatedBy = "user";
			return WatchedMovieDTO.Construct(watchedMovie);
		}

		public Task<List<WatchedMovieDTO>> GetAllAsync()
		{
			throw new NotImplementedException();
		}
	}
}

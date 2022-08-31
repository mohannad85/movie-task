using Microsoft.AspNetCore.Mvc;
using MoviePortal.ApplicationCore.Interfaces.Service;
using MoviePortal.Common.Movie.DTO;

namespace MoviePortal.API.Controllers
{
	[Route("api/movies")]
	public class MovieController : ApiController
	{
		private readonly IMessagePublisher _messagePublisher;
		private readonly IMovieService _movieService;

		public MovieController(IMessagePublisher messagePublisher, IMovieService movieService)
		{
			_messagePublisher = messagePublisher ?? throw new ArgumentNullException(nameof(messagePublisher));
			_movieService = movieService ?? throw new ArgumentNullException(nameof(movieService));
		}

		[ProducesResponseType(StatusCodes.Status200OK)]
		[HttpGet]
		public async Task<List<MovieBasicInfoDTO>> GetMovies()
		{
			await _messagePublisher.Publish();
			return await _movieService.GetAllAsync();
		}
	}
}

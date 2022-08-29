using Microsoft.AspNetCore.Mvc;
using MoviePortal.ApplicationCore.Interfaces.Service;
using MoviePortal.ApplicationCore.Model.DTO;

namespace MoviePortal.API.Controllers
{
	[Route("api/movies")]
	public class MovieController : ApiController
	{
		private readonly IMovieService _movieService;

		public MovieController(IMovieService movieService)
		{
			_movieService = movieService;
		}

		[ProducesResponseType(StatusCodes.Status200OK)]
		[HttpGet]
		public async Task<List<MovieDTO>> GetMovies() =>
			await _movieService.GetAllAsync();
	}
}

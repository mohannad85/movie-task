using Microsoft.AspNetCore.Mvc;
using MoviePortal.ApplicationCore.Interfaces.Service;
using MoviePortal.ApplicationCore.Model.DTO;

namespace MoviePortal.API.Controllers
{
	[Route("api/movie-watches")]
	public class WatchedMovieController : Controller
	{
        private readonly IWatchedMovieService _watchedMovieService;

		public WatchedMovieController(IWatchedMovieService watchedMovieService)
		{
            _watchedMovieService = watchedMovieService ?? throw new ArgumentNullException(nameof(watchedMovieService));
		}

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<WatchedMovieDTO> CreateWatchedMovie([FromBody] WatchedMovieDTO dto) =>
            await _watchedMovieService.CreateWatchedMovieAsync(dto);
            
    }
}

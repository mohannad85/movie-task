using MoviePortal.ApplicationCore.Model.DTO;

namespace MoviePortal.ApplicationCore.Interfaces.Service
{
	public interface IWatchedMovieService
	{
		Task<WatchedMovieDTO> CreateWatchedMovieAsync(WatchedMovieDTO dto);
		Task<List<WatchedMovieDTO>> GetAllAsync();
	}
}

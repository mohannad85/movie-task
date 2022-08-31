using MoviePortal.Common.Movie.DTO;

namespace MoviePortal.ApplicationCore.Interfaces.Service
{
	public interface IMovieService
	{
		Task<List<MovieBasicInfoDTO>> GetAllAsync();
	}
}

using MoviePortal.ApplicationCore.Model.DTO;

namespace MoviePortal.ApplicationCore.Interfaces.Service
{
	public interface IMovieService
	{
		Task<List<MovieDTO>> GetAllAsync();
	}
}

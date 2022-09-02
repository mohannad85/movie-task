using Microsoft.EntityFrameworkCore;
using MoviePortal.ApplicationCore.Model;

namespace MoviePortal.ApplicationCore.Interfaces.Infrastructure
{
	public interface IMovieDbContext
	{
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
		public DbSet<WatchedMovie> WatchedMovies { get; set; }
		public DbSet<ReminderMovie> ReminderMovies { get; set; }
	}
}

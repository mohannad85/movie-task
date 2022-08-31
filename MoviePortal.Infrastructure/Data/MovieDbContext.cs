using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoviePortal.ApplicationCore.Model;

namespace MoviePortal.Infrastructure.Data
{
	public class MovieDbContext : IdentityDbContext<User>
	{
		public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
		{
		}

		public DbSet<WatchedMovie> WatchedMovies { get; set; } = null!;
		public DbSet<ReminderMovie> ReminderMovies { get; set; } = null!;

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}
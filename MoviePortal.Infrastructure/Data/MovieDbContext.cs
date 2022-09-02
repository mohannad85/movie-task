using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MoviePortal.ApplicationCore.Interfaces.Infrastructure;
using MoviePortal.ApplicationCore.Model;

namespace MoviePortal.Infrastructure.Data
{
	public class MovieDbContext : DbContext, IMovieDbContext
	{
		public MovieDbContext(DbContextOptions<MovieDbContext> options, IConfiguration configuration) : base(options)
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
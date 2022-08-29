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
	}
}
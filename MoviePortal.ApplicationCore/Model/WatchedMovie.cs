using DataAnnotationsExtensions;
using MoviePortal.ApplicationCore.Interfaces.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviePortal.ApplicationCore.Model
{
	public class WatchedMovie : IChangeTracker
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		public int Id { get; set; }
		public string MovieName { get; set; } = "";
		public int MovieId { get; set; }
		[Min(1), Max(5)]
		public int Rating { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public string? CreatedBy { get; set; }
		public string? UpdatedBy { get; set; }
	}
}

using MoviePortal.ApplicationCore.Interfaces.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviePortal.ApplicationCore.Model
{
	public class ReminderMovie : IChangeTracker
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		public int Id { get; set; }
		public string MovieName { get; set; } = "";
		public int MovieId { get; set; }
		public DateTime ReminderDate { get; set; }

		public DateTime? CreatedDate { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public string? CreatedBy { get; set; }
		public string? UpdatedBy { get; set; }
	}
}

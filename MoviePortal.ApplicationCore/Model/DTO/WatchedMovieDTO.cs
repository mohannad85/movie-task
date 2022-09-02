using DataAnnotationsExtensions;
using MoviePortal.ApplicationCore.DataAnnotations;

namespace MoviePortal.ApplicationCore.Model.DTO
{
	public class WatchedMovieDTO
	{
		public int Id { get; set; }
		public string MovieName { get; set; } = "";
		public int MovieId { get; set; }
		[Min(1), Max(5)]
		public int Rating { get; set; }
		[DateTime]
		public string? CreatedDate { get; set; }
		public string? CreatedBy { get; set; }
		[DateTime]
		public string? Updated { get; set; }
		public string? UpdatedBy { get; set; }

        public static WatchedMovieDTO Construct(WatchedMovie entity)
        {
            return new WatchedMovieDTO
            {
                Id = entity.Id,
                MovieName = entity.MovieName,
                MovieId = entity.MovieId,
                Rating = entity.Rating,
                CreatedDate = entity.CreatedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                CreatedBy = entity.CreatedBy,
                Updated = entity.UpdatedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                UpdatedBy = entity.UpdatedBy
            };
        }

        public static WatchedMovie Deconstruct(WatchedMovieDTO dto)
        {
            return new WatchedMovie
            {
                MovieName = dto.MovieName,
                MovieId = dto.MovieId,
                Rating = dto.Rating
            };
        }
    }
}

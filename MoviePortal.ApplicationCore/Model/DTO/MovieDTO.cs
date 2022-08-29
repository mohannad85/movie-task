namespace MoviePortal.ApplicationCore.Model.DTO
{
	public class MovieDTO
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Rating { get; set; }
		public MovieProviderDTO? MovieProvider { get; set; }
	}
}

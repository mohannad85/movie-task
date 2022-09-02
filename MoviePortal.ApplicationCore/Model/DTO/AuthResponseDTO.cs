namespace MoviePortal.ApplicationCore.Model.DTO
{
	public class AuthResponseDTO
	{
		public string? Token { get; set; }
		public bool IsAuthSuccessful { get; set; }
		public string Name { get; set; }
	}
}

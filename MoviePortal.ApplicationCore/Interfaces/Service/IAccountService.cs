using MoviePortal.ApplicationCore.Model.DTO;

namespace MoviePortal.ApplicationCore.Interfaces.Service
{
	public interface IAccountService
	{
		Task<AuthResponseDTO> ExternalLogin(string credential);
	}
}

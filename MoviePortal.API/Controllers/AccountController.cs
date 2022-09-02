using Microsoft.AspNetCore.Mvc;
using MoviePortal.ApplicationCore.Interfaces.Service;
using MoviePortal.ApplicationCore.Model.DTO;

namespace MoviePortal.API.Controllers
{
	[Route("api/accounts")]
	public class AccountController : ApiController
	{
		private readonly IAccountService _accountService;

		public AccountController(IAccountService accountService)
		{
			_accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
		}

		[HttpPost("external-login")]
		public async Task<AuthResponseDTO> ExternalLogin([FromBody] string credential) =>
			await _accountService.ExternalLogin(credential);
	}
}

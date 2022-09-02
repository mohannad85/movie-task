using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MoviePortal.ApplicationCore.Interfaces.Infrastructure;
using MoviePortal.ApplicationCore.Interfaces.Service;
using MoviePortal.ApplicationCore.Model;
using MoviePortal.ApplicationCore.Model.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MoviePortal.ApplicationCore.Service
{
	public class AccountService : IAccountService
	{
		private readonly IConfiguration _configuration;
		private readonly IConfigurationSection _jwtSettings;
		private readonly IConfigurationSection _goolgeSettings;
		private readonly ILogger<AccountService> _logger;

		public AccountService(IConfiguration configuration, ILogger<AccountService> logger)
		{
			_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
			_jwtSettings = _configuration.GetSection("JwtSettings");
			_goolgeSettings = _configuration.GetSection("Authentication:Google");
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task<AuthResponseDTO> ExternalLogin(string credential)
		{
			var payload = await VerifyGoogleToken(credential);
			if (payload == null)
				return new AuthResponseDTO { Token = null, IsAuthSuccessful = false };
			
			_logger.LogInformation("Valid authentication");
			var user = new User() 
			{
				Name = payload.Name,
				Email = payload.Email,
			};

			if (user == null)
				return new AuthResponseDTO { Token = null, IsAuthSuccessful = false };
			
			var token = await GenerateToken(user);
			return new AuthResponseDTO { Token = token, IsAuthSuccessful = true };
		}

		private async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(string credential)
		{
			try
			{
				var settings = new GoogleJsonWebSignature.ValidationSettings()
				{
					Audience = new List<string>() { _goolgeSettings["ClientId"] }
				};

				var payload = await GoogleJsonWebSignature.ValidateAsync(credential, settings);
				return payload;
			}
			catch (Exception ex)
			{
				_logger.LogError("Goodle Authentication Failed");
				return null;
			}
		}

		public async Task<string> GenerateToken(User user)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(this._jwtSettings["securityKey"]);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[] { new Claim("id", user.UserName), new Claim("", "viewer") }),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			var encrypterToken = tokenHandler.WriteToken(token);

			return encrypterToken;
		}
	}
}

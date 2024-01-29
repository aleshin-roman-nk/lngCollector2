using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using ThoughtzLand.Api.auth;
using ThoughtzLand.Core.Models.Common;
using ThoughtzLand.Core.Services;
using UserRegistry;

namespace JwtWebApiTutorial.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		//public static User user = new User();
		private readonly IConfiguration _configuration;
		private readonly UserService _userService;

		public AuthController(IConfiguration configuration, UserService userService)
		{
			_configuration = configuration;
			_userService = userService;
		}

		[HttpPost("register")]
		public async Task<ActionResult<User>> Register(UserDto request)
		{
			if (_userService.EmailExists(request.Email)) 
				return StatusCode(StatusCodes.Status409Conflict, $"Email {request.Email} already exists");

			CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

			var newUser = new User
			{
				email = request.Email,
				//name = request.Username,
				passwordHash = passwordHash,
				passwordSalt = passwordSalt
			};

			//user.Username = request.Username;
			//user.PasswordHash = passwordHash;
			//user.PasswordSalt = passwordSalt;

			newUser = _userService.Create(newUser);

			var tok = CreateToken(newUser);

			//SetRefreshToken(newUser, GenerateRefreshToken());

			return Ok(new
			{
				token = tok,
				authUser = new AuthorizedUser(id: newUser.id.ToString(), name: newUser.name, email: newUser.email)
			});
		}

		[HttpPost("login")]
		public async Task<ActionResult<string>> Login(UserDto request)
		{
			//var user = _userService.GetUserByName(request.Username);

			//if (user == null)
			//{
			//	user == _userService.GetUserByEmail(request.Email);
			//}

			var user = _userService.GetUserByEmail(request.Email);
			if (user == null)
			{
				return BadRequest("User not found");
			}

			if (!VerifyPasswordHash(request.Password, user.passwordHash, user.passwordSalt))
			{
				return BadRequest("Wrong password");
			}

			string tok = CreateToken(user);

			//SetRefreshToken(user, GenerateRefreshToken());

			return Ok(new
			{
				token = tok,
				authUser = new AuthorizedUser(id: user.id.ToString(), name: user.name, email: user.email)
			});
		}

		//[HttpPost("refresh-token")]
		//public async Task<ActionResult<string>> RefreshToken()
		//{
		//	var refreshToken = Request.Cookies["refreshToken"];

		//	if (!user.RefreshToken.Equals(refreshToken))
		//	{
		//		return Unauthorized("Invalid Refresh Token.");
		//	}
		//	else if (user.TokenExpires < DateTime.Now)
		//	{
		//		return Unauthorized("Token expired.");
		//	}

		//	string token = CreateToken(user);
		//	SetRefreshToken(GenerateRefreshToken());

		//	return Ok(token);
		//}

		//private RefreshToken GenerateRefreshToken()
		//{
		//	var refreshToken = new RefreshToken
		//	{
		//		Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
		//		Expires = DateTime.Now.AddDays(7),
		//		Created = DateTime.Now
		//	};

		//	return refreshToken;
		//}

		//private void SetRefreshToken(User user, RefreshToken newRefreshToken)
		//{
		//	var cookieOptions = new CookieOptions
		//	{
		//		HttpOnly = true,
		//		Expires = newRefreshToken.Expires
		//	};
		//	Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

		//	user.refreshToken = newRefreshToken.Token;
		//	user.tokenCreated = newRefreshToken.Created;
		//	user.tokenExpires = newRefreshToken.Expires;

		//	_userService.Update(user);
		//}

		private string CreateToken(User user)
		{
			List<Claim> claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.name),
				//new Claim(ClaimTypes.Role, "Admin"),
				new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
				new Claim(ClaimTypes.Email, user.email)
			};

			var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
				_configuration.GetSection("AppSettings:Token").Value));

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

			var token = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.Now.AddDays(1),
				signingCredentials: creds);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			using (var hmac = new HMACSHA512())
			{
				passwordSalt = hmac.Key;
				passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
			}
		}

		private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
		{
			using (var hmac = new HMACSHA512(passwordSalt))
			{
				var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
				return computedHash.SequenceEqual(passwordHash);
			}
		}
	}
}

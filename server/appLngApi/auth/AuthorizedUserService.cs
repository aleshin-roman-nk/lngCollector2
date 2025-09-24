using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UserRegistry;

namespace ThoughtzLand.Api.auth
{
	public class AuthorizedUserService : IAuthorizedUserService
	{
		private readonly IHttpContextAccessor httpContextAccessor;

		public AuthorizedUserService(IHttpContextAccessor httpContextAccessor)
		{
			this.httpContextAccessor = httpContextAccessor;
		}

		public AuthorizedUser Get()
		{

			string? userId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			string? userName = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value;
			string? userEmail = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;

			return new AuthorizedUser(
				id: userId,
				name: userName,
				email: userEmail
				);
		}
	}
}

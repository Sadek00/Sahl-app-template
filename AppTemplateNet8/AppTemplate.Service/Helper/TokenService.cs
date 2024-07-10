using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AppTemplate.Service.Helper
{
    public interface ITokenService
    {
        string GetUserMappingIdFromToken();
    }

    public class TokenService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserMappingIdFromToken()
        {
            var token = GetTokenFromHeader();
            if (string.IsNullOrEmpty(token))
            {
                return String.Empty; // or handle the absence of the token as needed
            }
            return GetUserMappingId(token);
        }

        private string GetTokenFromHeader()
        {
            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            if (authorizationHeader != null && authorizationHeader.StartsWith("Bearer "))
            {
                return authorizationHeader.Substring("Bearer ".Length).Trim();
            }
            return String.Empty;
        }

        private string GetUserMappingId(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            // Find the user_mapping_id claim
            var userMappingIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "user_mapping_id");

            // Return the value of the user_mapping_id claim if it exists
            if (userMappingIdClaim != null)
            {
                return userMappingIdClaim.Value;
            }
            return String.Empty;            
        }
    }
}

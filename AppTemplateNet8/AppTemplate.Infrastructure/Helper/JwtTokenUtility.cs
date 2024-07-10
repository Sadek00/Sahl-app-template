using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Infrastructure.Helper
{
    public interface IJwtTokenUtility
    {
        (string accessToken, string refreshToken) GenerateTokens(string userId, string username);
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }

    public class JwtTokenUtility : IJwtTokenUtility
    {       
        private readonly SymmetricSecurityKey _signingKey;

        public JwtTokenUtility(string secretKey)
        {
            _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
        }

        public (string accessToken, string refreshToken) GenerateTokens(string userId, string username)
        {
            var claims = new[] { new Claim("user_id", userId), new Claim("user_name", username) };
            var accessToken = GenerateAccessToken(claims);
            var refreshToken = GenerateRefreshToken();
            return (accessToken, refreshToken);
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,
                ValidateLifetime = false // Here we are saying that we don't care about the token's expiration date
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        /*
        private string GenerateAccessToken(string userId, string username)
        {
            var claims = new[] {
            //new Claim(ClaimTypes.NameIdentifier, userId),
            //new Claim(ClaimTypes.Name, username),
            new Claim("user_id", userId),
            new Claim("user_name", username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConstants.AccessTokenSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var accessToken = new JwtSecurityToken(
                _issuer,
                _audience,
                claims,
                expires: DateTime.Now.AddHours(12), // Adjust the expiration as needed
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(accessToken);
        }
        private string GenerateRefreshToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConstants.RefreshTokenSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var refreshToken = new JwtSecurityToken(
                _issuer,
                _audience,
                expires: DateTime.Now.AddDays(2), // Refresh token may have a longer expiration
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(refreshToken);
        }
        */
    }
}

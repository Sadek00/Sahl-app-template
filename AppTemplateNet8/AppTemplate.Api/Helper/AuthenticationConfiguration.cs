using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AppTemplate.Api.Helper
{
    public static class AuthenticationConfiguration
    {
        public static void ConfigureAuthentication(this IServiceCollection services, string secret, string issuer, string audiance)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                //options.SaveToken = true;               // This is for central auth service
                //options.RequireHttpsMetadata = false;   // This is for central auth service
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //ValidateIssuer = true,
                    //ValidateAudience = true,
                    ValidateIssuer = false,             // This is for central auth service
                    ValidateAudience = false,           // This is for central auth service
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer, // Your authentication server's issuer
                    ValidAudience = audiance, // Your API's audience
                    //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret))
                };
            });
        }
    }
}

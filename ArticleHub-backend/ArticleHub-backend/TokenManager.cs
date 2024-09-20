using ArticleHub_backend.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace ArticleHub_backend
{
    public class TokenManager
    {


        private static readonly string Secret = "your-new-secret-key-that-is-16-bytes-long"; // 16 bytes = 128 bits

        public static string GenerateToken(string email, string isDeletable)
        {
            var key = Encoding.ASCII.GetBytes(Secret); // Ensure the key is at least 128 bits
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Email, email),
                new Claim("IsDeletable", isDeletable)
            }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtSecurityToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                if (jwtSecurityToken == null)
                {
                    return null;
                }
                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret))
                };
                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);
                return principal;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        //public static TokenClaim ValidateToken(string rawToken)
        //{
        //    TokenClaim tokenClaim = new TokenClaim();
        //    string[] array = rawToken.Split(' ');
        //    var token = array[1];
        //    ClaimsPrincipal principal = GetPrincipal(token);
        //    Claim email = principal.FindFirst(ClaimTypes.Email);
        //    tokenClaim.Email = email.Value;
        //    Claim isDeletable = principal.FindFirst("isDeletable");
        //    tokenClaim.isDeletable = isDeletable.Value;
        //    return tokenClaim;
        //}

        public static TokenClaim ValidateToken(string rawToken)
        {
            TokenClaim tokenClaim = new TokenClaim();
            string[] array = rawToken.Split(' ');
            var token = array[1];
            ClaimsPrincipal principal = GetPrincipal(token);

            // Check if the email claim exists
            Claim email = principal?.FindFirst(ClaimTypes.Email);
            if (email != null)
            {
                tokenClaim.Email = email.Value;
            }

            // Check if the isDeletable claim exists
            Claim isDeletable = principal?.FindFirst("IsDeletable");
            if (isDeletable != null)
            {
                tokenClaim.isDeletable = isDeletable.Value;
            }

            return tokenClaim;
        }


    }
}
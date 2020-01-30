using HRInventories.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HRInventories
{
    public class JWTUserModel : IdentityUser<int>
    {
        public string DisplayName { get; set; }
        //public string Role { get; set; }
        public string Access_Token { get; set; }
        public string Refresh_Token { get; set; }
        public bool IsAdmin { get; set; }


        public JWTUserModel(User model)
        {
            Id = Convert.ToInt32(model.Id);
            UserName = model.UserName;
            Email = model.Email;
            DisplayName = model.DisplayName;
            IsAdmin = model.isAdmin;
        }

        public JWTUserModel() { }
    }



    public class RefreshTokenModel
    {
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
    }

    public static class JWTSettings
    {
        public static string ValidIssuer { get; set; }
        public static string ValidAudience { get; set; }

        public static string IssuerSigningKey { get; set; }

        public static SymmetricSecurityKey SigningKey { get; set; }

        static JWTSettings()
        {
            //TODO: shift this to settings file.
            //configuration.Bind("JWTSettings", settings);
            ValidAudience = "https://synerzip.com";
            ValidIssuer = "https://synerzip.com/Issuer";
            IssuerSigningKey = "H0R4Oy8JQsVyIJfP3uy7qu0esa0OBvzS";
            SigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(IssuerSigningKey));
        }

        public static JWTUserModel GetJWTUser(User model)
        {
            JWTUserModel user = new JWTUserModel(model);
            user.Refresh_Token = GenerateRefreshToken();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Upn, user.DisplayName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.DisplayName),
                
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				//new Claim(ClaimTypes.Role, user.Role),
				new Claim(ClaimTypes.Hash, user.Refresh_Token),
            };

            user.Access_Token = CreateJWTToken(claims);
            return user;
        }


        public static JWTUserModel GetJWTUser(IEnumerable<Claim> claims)
        {
            JWTUserModel user = new JWTUserModel();
            user.UserName = claims.FirstOrDefault(k => k.Type == ClaimTypes.Upn)?.Value;
            user.Email = claims.FirstOrDefault(k => k.Type == ClaimTypes.Email)?.Value;
            int.TryParse(claims.FirstOrDefault(k => k.Type == ClaimTypes.NameIdentifier)?.Value, out int userId);
            int.TryParse(claims.FirstOrDefault(k => k.Type == ClaimTypes.GroupSid)?.Value, out int clientId);
            //user.Role = claims.FirstOrDefault(k => k.Type == ClaimTypes.Role)?.Value;
            user.DisplayName = claims.FirstOrDefault(k => k.Type == ClaimTypes.Name)?.Value;
            user.Refresh_Token = claims.FirstOrDefault(k => k.Type == ClaimTypes.Hash)?.Value;
            user.Id = userId;

            return user;
        }

        public static JWTUserModel GetNewAccessToken(RefreshTokenModel token)
        {
            var user = GetUserFromToken(token.AccessToken);
            if (user != null && token.RefreshToken == user.Refresh_Token)
            {
                user.Refresh_Token = GenerateRefreshToken();
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Upn, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.DisplayName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Hash, user.Refresh_Token),
                };

                user.Access_Token = CreateJWTToken(claims);
                return user;
            }
            //Log.Error($"GetNewAccessToken()- Can not generate new access token from refresh token {token.RefreshToken}. User {user?.Id} and Org {user?.OrgId} ");
            return null;
        }

        private static string CreateJWTToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(IssuerSigningKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(30));

            var token = new JwtSecurityToken(issuer: ValidIssuer, audience: ValidAudience, claims: claims, expires: expires, signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters
            {
                ValidIssuer = ValidIssuer,
                ValidAudience = ValidAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(IssuerSigningKey)),

                //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
                ClockSkew = TimeSpan.Zero // remove delay of token when expire
            };
        }

        private static string GenerateRefreshToken(int size = 32)
        {
            var randomNumber = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public static JWTUserModel GetUserFromToken(string token)
        {
            JWTUserModel user = null;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(IssuerSigningKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            try
            {
                var tokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidAudience = ValidAudience,
                    ValidIssuer = ValidIssuer,
                    IssuerSigningKey = creds.Key,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false
                };

                var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();

                var identity = handler.ValidateToken(token, tokenValidationParameters, out Microsoft.IdentityModel.Tokens.SecurityToken validatedToken);

                if (identity.Identity.IsAuthenticated)
                {
                    user = GetJWTUser(identity.Claims);
                    user.Access_Token = token;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return user;
        }

    }

}

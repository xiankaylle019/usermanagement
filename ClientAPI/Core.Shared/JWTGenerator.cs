using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using ClientAPI.Shared.DTOs;
using System.Security.Principal;
using System;
using ClientAPI.Models;
using Microsoft.IdentityModel.Tokens;

namespace ClientAPI.Core.Shared
{
    public class JWTGenerator
    {
         public object GenerateJWT(ApplicationUser user,  IConfiguration config, params string[] role){

            var tokenHandler = new JwtSecurityTokenHandler ();

            var identity = GetClaimsIdentity(user,role);

            var jwtkey = config["Jwt:Key"];
            // var issuer = config["Jwt:JwtIssuer"];

            var key = System.Text.Encoding.UTF8.GetBytes (jwtkey);

            var signingKey = new SymmetricSecurityKey(key);

            var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var securityToken = tokenHandler.CreateToken(new SecurityTokenDescriptor{
                Issuer = "Issuer",
                Audience = "Audience",
                SigningCredentials = creds,
                Subject = identity,
                Expires = System.DateTime.Now.AddDays(1),
                NotBefore = System.DateTime.Now
            });           

            var tokenString = tokenHandler.WriteToken(securityToken);

            return tokenString;
        }

        private ClaimsIdentity GetClaimsIdentity(ApplicationUser user, params string[] role){
           
            ClaimsIdentity identity = new ClaimsIdentity(

                new GenericIdentity(user.UserName, "Token"),
                new [] {
                    new Claim("ID", user.Id),
                    new Claim(ClaimTypes.Role, role[0])
                }

            );

            return identity;
        }
    }
}
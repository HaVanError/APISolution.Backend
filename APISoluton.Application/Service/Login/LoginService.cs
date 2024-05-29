using APISolution.Database.DatabaseContext;
using APISolution.Database.Entity;
using APISoluton.Application.Interface.Login.IAuthentication;
using APISoluton.Application.Interface.User.Commands;
using APISoluton.Application.ViewModel;
using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Service.Login
{
    public class LoginService : ILogin
    {
        private readonly DdConnect _db;
        private readonly IConfiguration _config;

        public LoginService(DdConnect db,IConfiguration config) { _db = db; _config = config; }
        public TokenResponse Authentication(LoginVM model)
        {
            var user = _db.Users.SingleOrDefault(x => x.Email == model.Email && x.Password == model.Password);
            if (user != null)
            {
                var accessToken = GenerateAccessToken(user, _config["Jwt:Secret"]);
                var refreshtoken = this.GenerateRefreshToken();
                var tokens = new ResfreshToken();
                tokens.Created = DateTime.UtcNow;
                tokens.Accesstoken = accessToken;
                tokens.Resfreshtoken = refreshtoken.Token;
                tokens.Expires = DateTime.UtcNow.AddMinutes(20);
                tokens.RoleName = user.Role.NameRole;
                tokens.idUser = user.Id;
                _db.ResfreshTokens.Add(tokens);
                _db.SaveChanges();
                return  new TokenResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshtoken.Token,
               };
            }
            else
            {
                return null;
            }
          
        }
        public  string GenerateAccessToken(APISolution.Database.Entity.User user, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var checkrole = _db.Roles.Where(x => x.idRole == user.IdRole).FirstOrDefault();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new[] { 
                        new Claim("id", user.Id.ToString()), 
                        new Claim(ClaimTypes.Role, checkrole.NameRole),
                    }),
                Expires = DateTime.UtcNow.AddMinutes(15), // Token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

     
     

        public  string GenerateAccessTokenFromRefreshToken(string refreshToken, string secret)
        {
            // Implement logic to generate a new access token from the refresh token
            // Verify the refresh token and extract necessary information (e.g., user ID)
            // Then generate a new access token

            // For demonstration purposes, return a new token with an extended expiry


            var checktokne = _db.ResfreshTokens.Where(x=>x.Resfreshtoken == refreshToken).FirstOrDefault();
            if(checktokne != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(secret);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(
                    new[] {
                        new Claim("id",checktokne.idUser.ToString()),
                        new Claim(ClaimTypes.Role, checktokne.RoleName),
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(15), // Extend expiration time
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
               
                var token = tokenHandler.CreateToken(tokenDescriptor);
                 checktokne.Accesstoken = tokenHandler.WriteToken(token); 
                _db.Entry(checktokne).State= Microsoft.EntityFrameworkCore.EntityState.Modified;
                _db.SaveChanges();
                return tokenHandler.WriteToken(token);
            }
            return "Erorr";
        }


        public TokenResponse RefreshTokens(string token)
        {
          
            var newAccessToken = GenerateAccessTokenFromRefreshToken(token, _config["Jwt:Secret"]);

            var response = new TokenResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = token // Return the same refresh token

            };
            return response;

        }
        private RefreshTokenVM GenerateRefreshToken()
        {
            var refreshToken = new RefreshTokenVM
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }



    }
}

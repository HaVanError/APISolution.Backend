using APISolution.Database.DatabaseContext;
using APISolution.Database.Entity;
using APISoluton.Application.Helper;
using APISoluton.Application.Interface.Login.IAuthentication;
using APISoluton.Application.Interface.IUsers.Commands;
using APISoluton.Application.ViewModel.LoginView;
using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.WebSockets;
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
        private readonly Appsetting _appsetting;

        public LoginService(DdConnect db,IConfiguration config,IOptions<Appsetting>appsetting) {
            _db = db; _config = config; _appsetting = appsetting.Value; }
        public TokenResponse Authentication(LoginVM model)
        {
            var user = _db.Users.SingleOrDefault(x => x.Email == model.Email && x.Password == model.Password);
            if (user != null)
            {
                var accessToken = GenerateAccessToken(user, _config.GetSection("Jwt:Secret").Value);
                var refreshtToken = this.GenerateRefreshToken();
                var checktoken = _db.ResfreshTokens.SingleOrDefault(x => x.idUser == user.Id);
                if (checktoken != null) {
                    var rest = RefreshTokens(checktoken.Resfreshtoken);
                    return new TokenResponse
                    {
                        AccessToken = rest.AccessToken,
                        RefreshToken = rest.RefreshToken,
                    };
                }
                else
                {
                    var tokens = new ResfreshToken();
                    tokens.Created = DateTime.UtcNow;
                    tokens.Accesstoken = accessToken;
                    tokens.Resfreshtoken = refreshtToken.Token;
                    tokens.Expires = DateTime.UtcNow.AddMinutes(20);
                    tokens.RoleName = user.Role.NameRole;
                    tokens.idUser = user.Id;
                    _db.ResfreshTokens.Add(tokens);
                    _db.SaveChanges();
                    return new TokenResponse
                    {
                        AccessToken = accessToken,
                        RefreshToken = refreshtToken.Token,
                    };
                }
            }
            else
            {
                return null;
            }
          
        }
        public  string GenerateAccessToken(APISolution.Database.Entity.User user,string Secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret);
            var checkrole = _db.Roles.Where(x => x.IdRole == user.IdRole).FirstOrDefault();
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
                    Expires = DateTime.UtcNow.AddMinutes(15),
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

        public bool Logout(string refreshToken)
        {
            var check = _db.ResfreshTokens.SingleOrDefault(x => x.Resfreshtoken == refreshToken);
            if (check != null) {
              _db.ResfreshTokens.Remove(check);
                _db.SaveChanges();
                return true;
            }
            else 
            return false;
        }

        public TokenResponse RefreshTokens(string token)
        {
          
            var newAccessToken = GenerateAccessTokenFromRefreshToken(token, _config.GetSection("Jwt:Secret").Value);

            var response = new TokenResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = token 

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

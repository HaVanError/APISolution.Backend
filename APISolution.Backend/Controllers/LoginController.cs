using APISolution.Database.Entity;
using APISoluton.Application.Helper;
using APISoluton.Application.Interface.Login.IAuthentication;
using APISoluton.Application.Interface.User.Commands;
using APISoluton.Application.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace APISolution.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogin _login;
        private readonly Appsetting _appsetting;
             
        public LoginController(ILogin login ,IOptions<Appsetting> appsetting)
        {
            _login = login;
            _appsetting = appsetting.Value;
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Authentication(LoginVM model)
        {
            var kiemtra =_login.Authentication(model);
         
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                //Expires = kiemtra.RefreshToken.,
                Secure = true,
                IsEssential = true

            };
            Response.Cookies.Append("refreshToken", kiemtra.RefreshToken, cookieOptions);
           
            return Ok(kiemtra);
            
        }
        [HttpPost("refresh")]
        public IActionResult Refreshtoken( )
        {
            var refreshTokenCookie = Request.Cookies["refreshToken"];
           
            if (refreshTokenCookie == string.Empty)
            {
                return Unauthorized("Invalid Refresh Token.");
            }
          

            //  var res = GenerateAccessTokenFromRefreshToken(refreshToken, _appsetting.key);
            var refreshtoken = _login.RefreshTokens(refreshTokenCookie);
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(1),
                Secure = true,
                IsEssential = true,
               
            };
            Response.Cookies.Append("refreshToken", refreshTokenCookie, cookieOptions);

            return Ok(refreshtoken);
        }
        
    }
}

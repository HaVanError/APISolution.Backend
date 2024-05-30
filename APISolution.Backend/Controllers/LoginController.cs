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
using System.Net.WebSockets;
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
            var kiemTra =_login.Authentication(model);
            var setCookie =  SetOptionCookies();
            Response.Cookies.Append("refreshToken", kiemTra.RefreshToken,setCookie);
            return Ok(kiemTra);
        }
        [HttpPost("refresh")]
        public IActionResult Refreshtoken( )
        {
            var refreshTokenCookie = Request.Cookies["refreshToken"];
           
            if (refreshTokenCookie == string.Empty)
            {
                return Unauthorized("Invalid Refresh Token.");
            }
            var reFreshToken = _login.RefreshTokens(refreshTokenCookie);
            var setCookie = SetOptionCookies();
            Response.Cookies.Append("refreshToken", refreshTokenCookie, setCookie);

            return Ok(reFreshToken);
        }
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var refreshTokenCookie = Request.Cookies["refreshToken"];
           var removeTokenDb = _login.Logout(refreshTokenCookie);
            if(removeTokenDb == true){
                DeleteCookie();
                return Ok("Thoat thanh cong");
            }
            else
            {
                return NoContent();
            }
        }
        [NonAction]
        public string DeleteCookie()
        {
            Response.Cookies.Delete("refreshToken");
            return "Cookies are Deleted";
        }
        [NonAction]
        public CookieOptions SetOptionCookies()
        {
           return  new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7),
                Secure = true,
                IsEssential = true

            };
        }
    }
}

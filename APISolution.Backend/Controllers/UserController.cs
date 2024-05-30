
using APISolution.Database.Entity;
using APISoluton.Application.Interface.User.Commands;
using APISoluton.Application.Interface.User.Queries;
using APISoluton.Application.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APISolution.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _user;
        private readonly IUsers _users;
        public UserController(IUser user, IUsers users) { 
        
            _user = user;
            _users = users;
        }
        [HttpPost]
        
        public async Task<IActionResult> Add(UserVM user)
        {
            return Ok( await _user.CreatUser(user));
        }
        [HttpGet]
        [Authorize]
       
        public async Task<IActionResult> GetUser()
        {
            return Ok(await _users.GetListUsers());
        }
    }
}

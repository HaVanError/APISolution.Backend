﻿using APISolution.Database.Entity;
using APISoluton.Application.Interface.IUsers.Queries;
using APISoluton.Application.Interface.IUsers.Commands;
using APISoluton.Application.Service.CacheServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;
using APISoluton.Database.ViewModel.UserView;
using APISoluton.Database.ViewModel.UserView.UserViewShow;
using System.Drawing.Printing;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace APISolution.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserCommaind _user;
        private readonly IUsersQueries _users;
        private readonly CacheServices _cache; // DI MemoryCache Vào Class
        private readonly static string key = "User";
            
        public UserController(IUserCommaind user, IUsersQueries users, CacheServices cache) {

            _user = user;
            _users = users;
            _cache = cache;
        }
        [HttpPost]
        // [Authorize(Roles = "Admin")]
      
        public async Task<ActionResult> Add([FromForm] UserVM user)
        {
            try
            {
                
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (user == null)
                {
                    return BadRequest(ModelState);
                }
                var addUser =  await _user.CreatUser(user);
                return StatusCode(StatusCodes.Status201Created, user);

            }
            catch (Exception ex) {

                return StatusCode(StatusCodes.Status500InternalServerError, user);
            }

        }
        [HttpGet("{name}")]
        //   [Authorize(Roles ="Admin")]
        public async Task<ActionResult<User>> GetUserById(string name)
        {
           
            var checkcache = _cache.Get<User>(key);
            if (checkcache == null)
            {
                var item = await _users.GetUserByName(name);
                if (item == null)
                {
                    return NotFound();
                }
                if (name == null)
                {
                    return NotFound();
                }
                return item;
            }
            else
            {
                return checkcache;
            }

        }
        [HttpGet]
        //   [Authorize(Roles ="Admin")]

        public async Task<ActionResult<UserVMShowAll>> GetUser(int pageNumber = 1, int pageSize=5)
        {
            
          string  keyss = $"user_{pageNumber}_{pageSize}";
          
            var exit = _cache.GetList<UserVMShowAll>(keyss);
            
            if (exit == null)
            {
                var listUser = await _users.GetListUsers(pageNumber, pageSize);

                return Ok(listUser);
            }
            else
            {
                return Ok(exit);
            }
        }
        [HttpDelete("{id:int}", Name = "DeleteUser")]
        //  [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            if (id == 0) {
                return BadRequest();
            }
            else
            {
                await _user.DeleteUser(id);
                return NoContent();
            }       
        }
        [HttpPut]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser(int id, UserVM user)
        {
            if (id == 0)
            {
                 return BadRequest();
            }
            else
            {
                await _user.UpdateUser(id, user);
             
                return  NoContent();
            }
        }

    }
}

using APISolution.Database.Entity;
using APISoluton.Application.Interface.IRole.Commands;
using APISoluton.Application.Interface.IRole.Queries;
using APISoluton.Application.Service.CacheServices;
using APISoluton.Database.ViewModel.RoleView;
using APISoluton.Database.ViewModel.UserView.UserViewShow;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Drawing.Printing;

namespace APISolution.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleCommand _commandRole;
        private readonly IRolesQueries _queriesRole;
        private  static string _key = "Role";
        private readonly CacheServices _cache;
        public RoleController(IRoleCommand role, IRolesQueries roles, CacheServices cache)
        {
            _commandRole = role;
            _queriesRole = roles;
            _cache = cache;
        }
        [HttpPost]
      // [Authorize(Roles = "Admin")]
        public async Task <IActionResult>Add(RoleVM vn)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (vn == null)
                {
                    return BadRequest(ModelState);
                }
                var addUser = await _commandRole.Add(vn);
                _cache.Remove(_key);
                return StatusCode(StatusCodes.Status201Created, vn);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, vn);
            }  
        }
        [HttpGet]
       // [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Role>> Get(int pageNumber ,int pageSize)
        {
             _key = $"Role_{pageNumber}_{pageSize}";
            var cache = _cache.GetList<RoleVM>(_key);
            if (cache == null)
            {
                var listRole = await _queriesRole.GetAllRoless(pageNumber, pageSize);
                return Ok(listRole);
            }
            else
            {
                return Ok(cache);
            }
        }
        [HttpPut("{id:int}")]
      //  [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRole(int id , RoleVM model)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            else {
                await _commandRole.Update(model, id);
                _cache.Remove(_key);
                return NoContent();
            }
           
        }
        [HttpDelete]
      // [Authorize(Roles ="Admin")]
        public async Task <IActionResult> DeleteRole(int id)
        {
            if(id ==0)
            {
                return BadRequest();
            }
            await _commandRole.Delete(id);
            _cache.Remove(_key);
            return NoContent();
        }
    }
}

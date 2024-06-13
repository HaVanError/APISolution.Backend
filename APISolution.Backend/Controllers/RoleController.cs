using APISolution.Database.Entity;
using APISoluton.Application.Interface.Role.Commands;
using APISoluton.Application.Interface.Role.Queries;
using APISoluton.Application.ViewModel.RoleView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APISolution.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleCommand _role;
        private readonly IRolesQueries _roles;
        public RoleController(IRoleCommand role, IRolesQueries roles)
        {
            _role = role;
            _roles = roles;
        }
        [HttpPost]
      // [Authorize(Roles = "Admin")]
        public IActionResult Add(RoleVM vn)
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
                var addUser = _role.Add(vn);
                return StatusCode(StatusCodes.Status201Created, vn);

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, vn);
            }
          
        }
        [HttpGet]
       // [Authorize(Roles = "Admin")]
        public IActionResult Get()
        {
            return Ok(_roles.GetAllRoles());
        }
        [HttpPut("{id:int}")]
      //  [Authorize(Roles = "Admin")]
        public IActionResult UpdateRole(int id , RoleVM model)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            else {
                return Ok(_role.Update(model, id));
            }
           
        }
        [HttpDelete]
      // [Authorize(Roles ="Admin")]
        public IActionResult DeleteRole(int id)
        {
            if(id ==0)
            {
                return BadRequest();
            }
            return Ok(_role.Delete(id));
        }
    }
}

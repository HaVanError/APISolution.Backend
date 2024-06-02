using APISoluton.Application.Interface.Role.Commands;
using APISoluton.Application.Interface.Role.Queries;
using APISoluton.Application.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APISolution.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRole _role;
        private readonly IRoles _roles;
        public RoleController(IRole role, IRoles roles)
        {
            _role = role;
            _roles = roles;
        }
        [HttpPost]
       // [Authorize(Roles = "Admin")]
        public IActionResult Add(RoleVM vn)
        {
            return Ok (_role.Add(vn));
        }
        [HttpGet]
        
        public IActionResult Get()
        {
            return Ok(_roles.GetAllRoles());
        }
        [HttpPut("{id:int}")]
       // [Authorize(Roles = "Admin")]
        public IActionResult UpdateRole(int id , RoleVM model)
        {
            return Ok(_role.Update(model,id));
        }
        [HttpDelete]
       // [Authorize(Roles ="Admin")]
        public IActionResult DeleteRole(int id)
        {
            return Ok(_role.Delete(id));
        }
    }
}

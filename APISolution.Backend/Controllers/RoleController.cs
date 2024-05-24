using APISoluton.Application.Conmon;
using APISoluton.Application.IService;
using APISoluton.Application.ViewModel;
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
        public IActionResult Add(RoleVM vn)
        {
            return Ok (_role.Add(vn));
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_roles.GetAllRoles());
        }
    }
}

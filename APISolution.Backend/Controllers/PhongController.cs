using APISoluton.Application.Interface.LoaiPhong.Commands;
using APISoluton.Application.Interface.LoaiPhong.Queries;
using APISoluton.Application.Interface.Phong.Commands;
using APISoluton.Application.Interface.Phong.Queries;
using APISoluton.Application.ViewModel.PhongView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APISolution.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhongController : ControllerBase
    {
        private readonly IPhongCommand _phongCommand;
        private readonly IPhongQueries _phongQueries;
        public PhongController(IPhongCommand phongCommand, IPhongQueries phongQueries)
        {
            _phongCommand = phongCommand;
            _phongQueries = phongQueries;
        }
        [HttpPost]
        public async Task<IActionResult>AddPhong(PhongVM phong)
        {
            return Ok(await _phongCommand.AddPhong(phong));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPhong()
        {
            return Ok(await _phongQueries.GetAllPhong());
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAllPhongById(int id)
        {
            return Ok(await _phongQueries.GetPhongById(id));
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePhong(int id)
        {
            return Ok(await _phongCommand.RemovePhong(id));
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePhong(int id,PhongVM model)
        {
            return Ok(await _phongCommand.UpdatePhong(model,id));
        }
    }
}

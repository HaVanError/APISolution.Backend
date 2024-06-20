using APISolution.Database.ViewModel.PhongView;
using APISoluton.Application.Interface.Phong.Commands;
using APISoluton.Application.Interface.Phong.Queries;
using APISoluton.Application.Service.CacheServices;
using APISoluton.Database.ViewModel.PhongView;
using APISoluton.Database.ViewModel.PhongView.PhongViewShowClient;
using APISoluton.Database.ViewModel.UserView.UserViewShow;
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
        private readonly CacheServices _cache;
        public PhongController(IPhongCommand phongCommand, IPhongQueries phongQueries,CacheServices cache)
        {
            _phongCommand = phongCommand;
            _phongQueries = phongQueries;
            _cache = cache;
        }
        [HttpPost]
        public async Task<IActionResult>AddPhong(AddPhongView phong)
        {
           
            if (!ModelState.IsValid) { 
                return BadRequest(ModelState);
            }
            else
            {
             var phongs =  await _phongCommand.AddPhong(phong);
                return Ok(phongs);
            }
          
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPhong(int pageNumber, int pageSize)
        {
            string keyss = $"ListPhong_{pageNumber}_{pageSize}";
            var exit = _cache.GetList<PhongVMShow>(keyss);
            if (exit == null)
            {
                var listPhong = await _phongQueries.GetAllPhong(pageNumber, pageSize);

                return Ok(listPhong);
            }
            else
            {
                return Ok(exit);
            }
           
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAllPhongById(int id)
        {
            return Ok(await _phongQueries.GetPhongById(id));
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePhong(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            else
            {
                await _phongCommand.RemovePhong(id);
                return NoContent();
            }
           
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePhong(int id,PhongVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else if(id == 0)
            {
                return BadRequest();
            }
            else
            {
              await  _phongCommand.UpdatePhong(model,id);
                return NoContent();
            }
        }
    }
}

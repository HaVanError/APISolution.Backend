using APISolution.Database.ViewModel.PhongView;
using APISoluton.Application.Interface.IPhong.Commands;
using APISoluton.Application.Interface.IPhong.Queries;
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
        private readonly IPhongCommand _commandPhong;
        private readonly IPhongQueries _queriesPhong;
        private readonly CacheServices _cache;
        private static string _key = "Phong";
        public PhongController(IPhongCommand phongCommand, IPhongQueries phongQueries,CacheServices cache)
        {
            _commandPhong = phongCommand;
            _queriesPhong = phongQueries;
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
             var phongs =  await _commandPhong.AddPhong(phong);
                _cache.Remove(_key);
                return Ok(phongs);
            }
          
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPhong(int pageNumber, int pageSize)
        {
          _key = $"Phong_{pageNumber}_{pageSize}";
            var cache = _cache.GetList<PhongVMShow>(_key);
            if (cache == null)
            {
                var listPhong = await _queriesPhong.GetAllPhong(pageNumber, pageSize);

                return Ok(listPhong);
            }
            else
            {
                return Ok(cache);
            }
           
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAllPhongById(int id)
        {
            return Ok(await _queriesPhong.GetPhongById(id));
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
                await _commandPhong.RemovePhong(id);
                _cache.Remove(_key);
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
              await _commandPhong.UpdatePhong(model,id);
                _cache.Remove(_key);
                return NoContent();
            }
        }
    }
}

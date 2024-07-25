using APISolution.Database.ViewModel.ThanhToanView;
using APISoluton.Application.Interface.IPhong.Queries;
using APISoluton.Application.Interface.IThanhToan.Commands;
using APISoluton.Application.Interface.IThanhToan.Queries;
using APISoluton.Application.Service.CacheServices;
using APISoluton.Database.ViewModel.PhongView.PhongViewShowClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APISolution.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThanhToanController : ControllerBase
    {
        private readonly IThanhToanCommand _commandThanhToan;
        private readonly IThanhToanQueries _queriesThanhToan;
        private readonly CacheServices _cache;
        private static string _key = "ThanhToan";
        public ThanhToanController(IThanhToanCommand command,IThanhToanQueries queries,CacheServices cache)
        {
            _commandThanhToan = command;
            _queriesThanhToan = queries;
           _cache = cache;
        }
        [HttpPost]
        public async Task< IActionResult> add(ThanhToanAddVM moden)
        {
            if (moden == null) {
                return BadRequest();
             }
            _cache.Remove(_key);
            return Ok(await _commandThanhToan.ThanhToan(moden));
        }
          [HttpPut("{id}")]
        public async Task<IActionResult> DuyetThanhToan(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
       
            await _commandThanhToan.DuyetThanhToan(id);
            _cache.Remove(_key);
            return NoContent();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllThanhToan(int pageNumber , int pageSize)
        {
             _key = $"ThanhToan_{pageNumber}_{pageSize}";
            var cache = _cache.GetList<ThanhToanVM>(_key);
            if (cache == null)
            {
                var listThanhToan = await _queriesThanhToan.GetListThanhToan(pageNumber, pageSize);
                _cache.Set(_key, listThanhToan,TimeSpan.FromMinutes(39));
                return Ok(listThanhToan);
            }
            else
            {
              
                return Ok(cache);
            }
          
        }
    }
}

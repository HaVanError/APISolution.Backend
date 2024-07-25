using APISolution.Database.ViewModel;
using APISolution.Database.ViewModel.PhieuDatDichVuView.PhieuDatDichVuViewShow;
using APISoluton.Application.Interface.IDichVu.Queries;
using APISoluton.Application.Interface.IPhieuDatDichVu.Commands;
using APISoluton.Application.Interface.IPhieuDatDichVu.Queries;
using APISoluton.Application.Service.CacheServices;
using APISoluton.Database.ViewModel.DichVuView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APISolution.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuDatDichVuController : ControllerBase
    {
        private readonly IPhieuDatDichVuCommand _commandPhieuDatDichVu;
        private readonly IPhieuDatDichVuQueries _queriesPhieuDatDichVu;
        private readonly CacheServices _cache;
        private static string _key= "PhieuDatDV";
        public PhieuDatDichVuController(IPhieuDatDichVuCommand command, IPhieuDatDichVuQueries queries, CacheServices cache)
        {
            _commandPhieuDatDichVu = command;
            _queriesPhieuDatDichVu = queries;
            _cache = cache;
        }
        [HttpPost]
        public async Task<IActionResult> AddPhieuDatDV([FromForm]AddPhieuDatDichVuView model)
        {
            try
            {
              var ds=  await _commandPhieuDatDichVu.AddPhieuDatDichVu(model);
                _cache.Remove(_key);
                return Ok( ds);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi: {ex.Message}");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPhieuDatDV(int pageNumber, int pageSize)
        {
            _key = $"PhieuDatDV_{pageNumber}_{pageSize}";
            var cache = _cache.Get<List<DichVuVM>>(_key);
            if (cache == null)
            {
                var listPhieuDatDV = await _queriesPhieuDatDichVu.GetAllPhieuDichVu(pageNumber, pageSize);
                _cache.Set(_key, listPhieuDatDV, TimeSpan.FromMinutes(30));
                return Ok(listPhieuDatDV);
            }
            else
            {
                return Ok(cache);
            }
          
        }
    
        [HttpGet("getbyallphieudichvu")]
        public async Task<IActionResult> GetByPhieuDatDV([FromQuery] SearchViewPhieuDatDV model)
        {
            return Ok(await _queriesPhieuDatDichVu.GetByAllPhieuDichVu(model));
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePhieuDichVu(int id, AddPhieuDatDichVuView model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                await _commandPhieuDatDichVu.UpdatePhieuDatDichVu(id, model);
                _cache.Remove(_key);
                return NoContent();
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePhieuDichVu(int id)
        {
            if (id == 0)
            {
                return BadRequest(ModelState);
            }
            else
            {
                await _commandPhieuDatDichVu.DeletePhieuDatDichVu(id);
                _cache.Remove(_key);
                return NoContent();
            }
          
    }
    }
}

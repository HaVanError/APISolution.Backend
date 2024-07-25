using APISolution.Database.Entity;
using APISoluton.Application.Interface.IDichVu.Commands;
using APISoluton.Application.Interface.IDichVu.Queries;
using APISoluton.Application.Service.CacheServices;
using APISoluton.Database.ViewModel.DichVuView;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.Extensions.Caching.Memory;

namespace APISolution.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DichVuController : ControllerBase
    {
        private readonly IDichVuCommand _dichVuCommand;
        private readonly IDichVuQueries _dichVuQueries;
        private readonly CacheServices _cache;
        static string _key = "DichVu";
        public DichVuController(IDichVuCommand dichVuCommand, IDichVuQueries dichVuQueries, CacheServices cache)
        {
            _dichVuCommand = dichVuCommand;
            _dichVuQueries = dichVuQueries;
            _cache = cache;
        }
        [HttpPost]
        public async Task<IActionResult> AddDichVu(DichVuVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model == null)
            {
                return BadRequest(ModelState);
            }
            var dichvu = await _dichVuCommand.AddDichVu(model);
            _cache.Remove(_key);
            return StatusCode(StatusCodes.Status201Created, dichvu);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteDichVu(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            else
            {
                await _dichVuCommand.DeleteDichVu(id);
                _cache.Remove(_key);
                return NoContent();
            }
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateDichVu(int id, DichVuVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else if (id == 0)
            {
                return BadRequest();
            }
            else
            {
                await _dichVuCommand.UpdateDichVu(model, id);
                _cache.Remove(_key);
                return NoContent();
            }
        }
    
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdDichVu(int id )
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id ==0)
            {
                return BadRequest(ModelState);
            }
            var dichvu = await _dichVuQueries.GetByIdDichVu(id);
        return Ok(dichvu);

        }
        [HttpGet]
        public async Task<ActionResult<List<DichVuVM>>> GetAllDichVu(int pageNumber,int pageSize)
        {
            _key = $"DichVu_{pageNumber}_{pageSize}";
            var cache = _cache.Get<List<DichVuVM>>(_key);
            if (cache == null) {
                var listDichVu=await _dichVuQueries.GetAllDichVu(pageNumber, pageSize);
                _cache.Set(_key, listDichVu,TimeSpan.FromMinutes(30));
                return Ok(listDichVu);
            }
            else
            {
                return Ok(cache);
            }
        }

    }
}

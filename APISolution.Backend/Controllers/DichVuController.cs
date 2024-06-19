using APISolution.Database.Entity;
using APISoluton.Application.Interface.DichVu.Commands;
using APISoluton.Application.Interface.DichVu.Queries;
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
        private readonly IMemoryCache _cache;
        public DichVuController(IDichVuCommand dichVuCommand, IDichVuQueries dichVuQueries, IMemoryCache cache)
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
            var key = $"dichVu_{pageNumber}_{pageSize}";
            var baseDichVu = _cache.Get<List<DichVuVM>>(key);
            if (baseDichVu == null) {
                var listDichVu=await    _dichVuQueries.GetAllDichVu(pageNumber, pageSize);
                return Ok(listDichVu);
            }
            else
            {
                return Ok(baseDichVu);
            }

        }

    }
}

using APISolution.Database.ViewModel;
using APISolution.Database.ViewModel.PhieuDatDichVuView.PhieuDatDichVuViewShow;
using APISoluton.Application.Interface.IPhieuDatDichVu.Commands;
using APISoluton.Application.Interface.IPhieuDatDichVu.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APISolution.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuDatDichVuController : ControllerBase
    {
        private readonly IPhieuDatDichVuCommand _command;
        private readonly IPhieuDatDichVuQueries _queries;
        public PhieuDatDichVuController(IPhieuDatDichVuCommand command, IPhieuDatDichVuQueries queries)
        {
            _command = command;
            _queries = queries;
        }
        [HttpPost]
        public async Task<IActionResult> AddPhieuDatDV([FromForm]AddPhieuDatDichVuView model)
        {
            try
            {
              var ds=  await _command.AddPhieuDatDichVu(model);
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
           return Ok(_queries.GetAllPhieuDichVu(pageNumber, pageSize));
        }
    
        [HttpGet("getbyallphieudichvu")]
        public async Task<IActionResult> GetByPhieuDatDV([FromQuery] SearchViewPhieuDatDV model)
        {
            return Ok(await _queries.GetByAllPhieuDichVu(model));
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
                await _command.UpdatePhieuDatDichVu(id, model);
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
                await _command.DeletePhieuDatDichVu(id);
                return Ok();
            }
          
    }
    }
}

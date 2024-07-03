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
        private readonly IThanhToanCommand _command;
        private readonly IThanhToanQueries _queries;
        private readonly CacheServices _cache;
        public ThanhToanController(IThanhToanCommand command,IThanhToanQueries queries,CacheServices cache)
        {
            _command = command;
            _queries = queries;
           _cache = cache;
        }
        [HttpPost]
        public async Task< IActionResult> add(ThanhToanAddVM moden)
        {
            if (moden == null) {
                return BadRequest();
             }
            return Ok(await _command.ThanhToan(moden));
        }
          [HttpPut("{id}")]
        public async Task<IActionResult> DuyetThanhToan(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
           await  _command.DuyetThanhToan(id);
            return NoContent();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllThanhToan(int pageNumber , int pageSize)
        {
            string keyss = $"thanhToan_{pageNumber}_{pageSize}";
            var exit = _cache.GetList<ThanhToanVM>(keyss);
            if (exit == null)
            {
                var listThanhToan = await _queries.GetListThanhToan(pageNumber, pageSize);

                return Ok(listThanhToan);
            }
            else
            {
                return Ok(exit);
            }
          
        }
    }
}

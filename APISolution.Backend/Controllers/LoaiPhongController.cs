using APISolution.Database.Entity;
using APISoluton.Application.Interface.LoaiPhong.Commands;
using APISoluton.Application.Interface.LoaiPhong.Queries;
using APISoluton.Application.ViewModel.LoaiPhongView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APISolution.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiPhongController : ControllerBase
    {
        private readonly ILoaiPhongCommand _loaiPhongCommand;
        private readonly ILoaiPhongQueries _loaiPhongQueries;
        public LoaiPhongController(ILoaiPhongCommand loaiPhongCommand, ILoaiPhongQueries loaiPhongQueries)
        {
            _loaiPhongCommand = loaiPhongCommand;
            _loaiPhongQueries = loaiPhongQueries;
        }
        [HttpPost]
        public async Task<ActionResult<LoaiPhongVM>> AddLoai(LoaiPhongVM loaiPhong) { 
        
        return Ok( await _loaiPhongCommand.AddLoaiPhong(loaiPhong));
        }
        [HttpGet]
        public async Task<ActionResult<List<LoaiPhongVM>>> GetPhongs()
        {
            return Ok( await _loaiPhongQueries.GetAllLoaiPhong());
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<LoaiPhongVM>> RemoveLoaiPhong( int id)
        {
            var query = await _loaiPhongCommand.RemovePhong(id);
            return Ok(query) ;
          
        }

    }
}

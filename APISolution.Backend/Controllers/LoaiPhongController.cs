
using APISoluton.Application.Interface.ILoaiPhong.Commands;
using APISoluton.Application.Interface.ILoaiPhong.Queries;
using APISoluton.Application.Service.CacheServices;
using APISoluton.Database.ViewModel.LoaiPhongView;
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
        private readonly CacheServices _cacheServices;
        static string key = "LoaiPhong";
        public LoaiPhongController(ILoaiPhongCommand loaiPhongCommand, ILoaiPhongQueries loaiPhongQueries,CacheServices cacheServices )
        {
            _loaiPhongCommand = loaiPhongCommand;
            _loaiPhongQueries = loaiPhongQueries;
            _cacheServices = cacheServices;
        }
        [HttpPost]
        public async Task<ActionResult<LoaiPhongVM>> AddLoai(LoaiPhongVM loaiPhong) {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                await _loaiPhongCommand.AddLoaiPhong(loaiPhong);
                _cacheServices.Remove(key);
                return Created();
            }
        }
        [HttpGet]
        public async Task<ActionResult<List<LoaiPhongVM>>> GetLoaiPhongs()
        {
            var key = "loai";
            var cacherbase = _cacheServices.Get<List<LoaiPhongVM>>(key);
            if(cacherbase == null)
            {
                 var dsLoai = await _loaiPhongQueries.GetAllLoaiPhong();
                _cacheServices.Set(key, dsLoai,TimeSpan.FromMinutes(30));
                return Ok(dsLoai);
            }
            else
            {
                return cacherbase;
            }
           

        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<LoaiPhongVM>> RemoveLoaiPhong( int id)
        {
           
            if(id == 0)
            {
                return BadRequest();
            }
            else
            {
                await _loaiPhongCommand.RemovePhong(id);
                _cacheServices.Remove(key);
                return NoContent();
            }
          
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<LoaiPhongVM>> UpdateLoaiPhong(int id,LoaiPhongVM model)
        {

            if (id == 0)
            {
                return BadRequest();
            }
            else
            {
                await _loaiPhongCommand.UpdateLoaiPhong(model,id);
                _cacheServices.Remove(key);
                return NoContent();
            }

        }

    }
}

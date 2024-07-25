
using APISoluton.Application.Interface.ILoaiPhong.Commands;
using APISoluton.Application.Interface.ILoaiPhong.Queries;
using APISoluton.Application.Service.CacheServices;
using APISoluton.Database.ViewModel.LoaiPhongView;
using Microsoft.AspNetCore.Authorization;
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
        static string _key = "LoaiPhong";
        public LoaiPhongController(ILoaiPhongCommand loaiPhongCommand, ILoaiPhongQueries loaiPhongQueries,CacheServices cacheServices )
        {
            _loaiPhongCommand = loaiPhongCommand;
            _loaiPhongQueries = loaiPhongQueries;
            _cacheServices = cacheServices;
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<LoaiPhongVM>> AddLoai(LoaiPhongVM loaiPhong) {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                await _loaiPhongCommand.AddLoaiPhong(loaiPhong);
                _cacheServices.Remove(_key);
                return Created();
            }
        }
        [HttpGet]
        public async Task<ActionResult<List<LoaiPhongVM>>> GetLoaiPhongs()
        {
             _key = "loai";
            var cache = _cacheServices.Get<List<LoaiPhongVM>>(_key);
            if(cache == null)
            {
                 var dsLoai = await _loaiPhongQueries.GetAllLoaiPhong();
                _cacheServices.Set(_key, dsLoai,TimeSpan.FromMinutes(30));
                return Ok(dsLoai);
            }
            else
            {
                return cache;
            }
        }
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<LoaiPhongVM>> RemoveLoaiPhong( int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            else
            {
                await _loaiPhongCommand.RemovePhong(id);
                _cacheServices.Remove(_key);
                return NoContent();
            }
        }
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<LoaiPhongVM>> UpdateLoaiPhong(int id,LoaiPhongVM model)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            else
            {
                await _loaiPhongCommand.UpdateLoaiPhong(model,id);
                _cacheServices.Remove(_key);
                return NoContent();
            }
        }

    }
}

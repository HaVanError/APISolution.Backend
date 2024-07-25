using APISolution.Database.Entity;
using APISolution.Database.ViewModel;
using APISolution.Database.ViewModel.PhieuDatPhongView.PhieuDatPhongViewShow;
using APISoluton.Application.Interface.IPhieuDatPhong.Commands;
using APISoluton.Application.Interface.IPhieuDatPhong.Queries;
using APISoluton.Application.Service.CacheServices;
using APISoluton.Database.ViewModel.DichVuView;
using APISoluton.Database.ViewModel.PhieuDatPhongView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APISolution.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuDatPhongController : ControllerBase
    {
        private readonly IPhieuDatPhongCommand _conmandPhieuDatPhong;
        private readonly IPhieuDatPhongQueries _queriesPhieuDatPhong;
        private readonly CacheServices _cache;
        private static string _key = "PhieuDatPhong";
        public PhieuDatPhongController(IPhieuDatPhongCommand conmand, IPhieuDatPhongQueries queries,CacheServices cache)
        {
            _conmandPhieuDatPhong = conmand;
            _queriesPhieuDatPhong = queries;
            _cache = cache;
        }
        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> DatPhong([FromForm] AddPhieuDatPhongView model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                _cache.Remove(_key);
                return Ok(await _conmandPhieuDatPhong.DatPhong(model));
            }
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllPhieuDatPhong(int pageNumber, int pageSize)
        {
            _key = $"PhieuDatPhon_{pageNumber}_{pageSize}";
            var baseDP = _cache.Get<List<DichVuVM>>(_key);
            if (baseDP == null)
            {
                var listPhieuDatPhong = await _queriesPhieuDatPhong.GetAllPhieuDatPhong(pageNumber, pageSize);
                _cache.Set(_key, listPhieuDatPhong, TimeSpan.FromMinutes(30));
                return Ok(listPhieuDatPhong);
            }
            else
            {
                return Ok(baseDP);
            }
           
        }
        [HttpGet("getbyall")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByAllPhieuDatPhong([FromQuery]ViewSeach model)
        {
        
         return Ok(await _queriesPhieuDatPhong.GetByAllPhieuDatPhong(model));
        }
        [HttpPut("duyetdatphong/{idPhieuDat}")]'
           [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DuyetPhieuDatPhong(int idPhieuDat)
        {
            if(idPhieuDat == 0)
            {
                return BadRequest(ModelState);
            }
            else
            {
               await _conmandPhieuDatPhong.DuyetDatPhong(idPhieuDat);
                _cache.Remove(_key);
                return NoContent();
            }
        }
        [HttpPut("traphong/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> TraPhong(int id)
        {
            if (id == 0)
            {
                return BadRequest(ModelState);
            }
            else
            {
                await _conmandPhieuDatPhong.TraDatPhong(id);
                _cache.Remove(_key);
                return NoContent();
            }
        }
    }
}

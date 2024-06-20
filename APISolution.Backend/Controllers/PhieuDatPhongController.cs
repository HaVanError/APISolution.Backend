using APISolution.Database.Entity;
using APISolution.Database.ViewModel;
using APISolution.Database.ViewModel.PhieuDatPhongView.PhieuDatPhongViewShow;
using APISoluton.Application.Interface.IPhieuDatPhong.Commands;
using APISoluton.Application.Interface.IPhieuDatPhong.Queries;


using APISoluton.Database.ViewModel.PhieuDatPhongView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APISolution.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuDatPhongController : ControllerBase
    {
        private readonly IPhieuDatPhongCommand _conmand;
        private readonly IPhieuDatPhongQueries _queries;
        public PhieuDatPhongController(IPhieuDatPhongCommand conmand, IPhieuDatPhongQueries queries)
        {
            _conmand = conmand;
            _queries = queries;
        }
        [HttpPost]
        public async Task<IActionResult> DatPhong([FromForm] AddPhieuDatPhongView model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(await _conmand.DatPhong(model));
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPhieuDatPhong(int pageNumber, int pageSize)
        {
            return Ok(await _queries.GetAllPhieuDatPhong(pageNumber, pageSize));
        }
        [HttpGet("getbyall")]
        public async Task<IActionResult> GetByAllPhieuDatPhong([FromQuery]ViewSeach model)
        {
         //  await _queries.GetByAllPhieuDatPhong(model);
         return Ok(await _queries.GetByAllPhieuDatPhong(model));
        }
        [HttpPut("duyetdatphong/{idPhieuDat}")]
 
        public async Task<IActionResult> DuyetPhieuDatPhong(int idPhieuDat)
        {
            if(idPhieuDat == 0)
            {
                return BadRequest(ModelState);
            }
            else
            {
               await _conmand.DuyetDatPhong(idPhieuDat);
                return NoContent();
            }
        }
        [HttpPut("traphong/{id:int}")]
     
        public async Task<IActionResult> TraPhong(int id)
        {
            if (id == 0)
            {
                return BadRequest(ModelState);
            }
            else
            {
                await _conmand.TraDatPhong(id);
                return NoContent();
            }
        }
    }
}

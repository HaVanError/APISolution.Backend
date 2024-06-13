using APISolution.Database.Entity;
using APISoluton.Application.Interface.DichVu.Commands;
using APISoluton.Application.Interface.DichVu.Queries;
using APISoluton.Application.ViewModel.DichVuView;
using APISoluton.Application.ViewModel.UserView.UserViewShow;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public DichVuController(IDichVuCommand dichVuCommand, IDichVuQueries dichVuQueries,IMemoryCache cache)
        {
         _dichVuCommand = dichVuCommand;
         _dichVuQueries = dichVuQueries;
            _cache = cache;
        }
        [HttpPost]
        public async Task<IActionResult> AddDichVu(DichVuVM model)
        {
            //try
            //{
            //    if (!ModelState.IsValid)
            //    {
            //        return BadRequest(ModelState);
            //    }
            //    if (model == null)
            //    {
            //        return BadRequest(ModelState);
            //    }
            //    var addUser = await _dichVuCommand.AddDichVu(model);
            //    //_cache.Remove("items/DichVu");
            //    return StatusCode(StatusCodes.Status201Created, model);

            //}
            //catch (Exception ex)
            //{

            //    return StatusCode(StatusCodes.Status500InternalServerError, model);
            //}
            if (!ModelState.IsValid) {
            return BadRequest(ModelState);

            }if (model == null)
            {
                return BadRequest(ModelState);
            }
            var dichvu = await _dichVuCommand.AddDichVu(model);
            _cache.Set("items/DichVu",dichvu);
            return StatusCode(StatusCodes.Status201Created, dichvu);
           
        }
        [HttpGet]
        public async Task<ActionResult<List<DichVuVM>>> GetAllDichVu()
        {
            if (!_cache.TryGetValue("items/DichVu", out List<DichVuVM> items))
            {
                items = await _dichVuQueries.GetAllDichVu();
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                    SlidingExpiration = TimeSpan.FromMinutes(2)
                };
                _cache.Set("items/DichVu", items, cacheEntryOptions);
            }
            return items.ToList();
       
        }
    }
}

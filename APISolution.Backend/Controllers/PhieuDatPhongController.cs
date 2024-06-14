﻿using APISoluton.Application.Interface.PhieuDatPhong.Commands;
using APISoluton.Application.Interface.PhieuDatPhong.Queries;
using APISoluton.Application.ViewModel.PhieuDatPhongView;
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
        public PhieuDatPhongController(IPhieuDatPhongCommand conmand , IPhieuDatPhongQueries queries)
        {
            _conmand = conmand;
            _queries = queries;
        }
        [HttpPost]
        public async Task<IActionResult> DatPhong(PhieuDatPhongVM model)
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
        public  async Task<IActionResult> GetAllPhieuDatPhong()
        {
            return Ok(await _queries.GetAllPhieuDatPhong());
        }
    }
}
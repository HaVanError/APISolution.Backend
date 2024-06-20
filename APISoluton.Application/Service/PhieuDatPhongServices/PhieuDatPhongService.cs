using APISolution.Database.DatabaseContext;
using APISolution.Database.Entity;
using APISolution.Database.Enum;
using APISolution.Database.ViewModel.PhieuDatPhongView.PhieuDatPhongViewShow;
using APISoluton.Application.Interface.PhieuDatPhong.Commands;
using APISoluton.Application.Interface.PhieuDatPhong.Queries;
using APISoluton.Database.ViewModel.PhieuDatPhongView;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Service.PhieuDatPhongServices
{
    public class PhieuDatPhongService : IPhieuDatPhongCommand,IPhieuDatPhongQueries
    {
        private readonly DdConnect _db;
        private readonly IMapper _mapper;
        public PhieuDatPhongService(DdConnect db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<PhieuDatPhongVM> DatPhong(AddPhieuDatPhongView mode)
        {
            var map = _mapper.Map<PhieuDatPhong>(mode);
            var check = await _db.PhieuDatPhongs.FirstOrDefaultAsync(x => x.IdPhieuDatPhong == map.IdPhieuDatPhong);

            if (check != null)
            {
                return null;
            }
            var checkphong = await _db.Phongs.FirstOrDefaultAsync(x => x.IdPhong == map.IdPhong);
            if (checkphong != null && checkphong.StatusPhong ==StatusPhong.Empty)
                {

                    var phieuDat = new PhieuDatPhong();
                    phieuDat.TenPhong = checkphong.Name;
                    phieuDat.IdPhong = checkphong.IdPhong;
                    phieuDat.TenNguoiDat = map.TenNguoiDat;
                    phieuDat.Status = APISolution.Database.Enum.TrangThaiPhieuDatPhong.Pending;
                    phieuDat.GiaPhong = checkphong.GiaPhong.ToString();
                    phieuDat.SoDienThoai = map.SoDienThoai;
                    checkphong.StatusPhong = StatusPhong.NotEmpty;
                    _db.PhieuDatPhongs.Add(phieuDat);
                    _db.Entry(checkphong).State =EntityState.Modified;
                    await _db.SaveChangesAsync();
                
                    return new PhieuDatPhongVM
                    {
                        IdPhong = checkphong.IdPhong,
                        TenPhong = checkphong.Name,
                        TenNguoiDat = phieuDat.TenNguoiDat,
                        SoDienThoai = phieuDat.SoDienThoai,
                        GiaPhong = checkphong.GiaPhong.ToString(),
                        Status = phieuDat.Status
                    };
                }
                return null;
        }
        public async Task<List<PhieuDatPhongVM>> GetAllPhieuDatPhong(int pageNumber, int pageSize)
        {
            var query = (from PhieuDatPhong in _db.PhieuDatPhongs
                         join Phong in _db.Phongs on PhieuDatPhong.IdPhong equals Phong.IdPhong
                         select new PhieuDatPhongVM
                         {
                             IdPhong = Phong.IdPhong,
                             TenPhong = PhieuDatPhong.TenPhong,
                             TenNguoiDat = PhieuDatPhong.TenNguoiDat,
                             SoDienThoai = PhieuDatPhong.TenNguoiDat,
                             GiaPhong = Phong.GiaPhong.ToString(),
                             Status = PhieuDatPhong.Status,
                         }).Skip((pageNumber-1)*pageSize).Take(pageSize);
            return await  query.ToListAsync();
        }
        public async Task<PhieuDatPhongVM> GetByIdPhieuDatPhong(int id)
        {
            var check =  await _db.PhieuDatPhongs.FindAsync(id);
            if (check != null)
            {
                var map = _mapper.Map<PhieuDatPhongVM>(check);
                return map;
            }
            else { 
            return null;
            }
        }
    }
}

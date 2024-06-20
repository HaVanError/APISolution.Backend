using APISolution.Database.DatabaseContext;
using APISolution.Database.Entity;
using APISolution.Database.Enum;
using APISolution.Database.ViewModel;
using APISolution.Database.ViewModel.PhieuDatPhongView.PhieuDatPhongViewShow;
using APISoluton.Application.Interface.IPhieuDatPhong.Commands;
using APISoluton.Application.Interface.IPhieuDatPhong.Queries;

using APISoluton.Database.ViewModel.PhieuDatPhongView;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
                   // checkphong.StatusPhong = StatusPhong.NotEmpty;
                    _db.PhieuDatPhongs.Add(phieuDat);
                    //_db.Entry(checkphong).State =EntityState.Modified;
                    await _db.SaveChangesAsync();
                
                    return new PhieuDatPhongVM
                    {
                        IdPhong = checkphong.IdPhong,
                        TenPhong = checkphong.Name,
                        TenNguoiDat = phieuDat.TenNguoiDat,
                        SoDienThoai = phieuDat.SoDienThoai,
                        GiaPhong = checkphong.GiaPhong.ToString(),
                        trangthai = phieuDat.Status.ToString(),
                    };
                }
                return null;
        }

        public async Task DuyetDatPhong(int idPhieuDatPhong)
        {
            var check = await _db.PhieuDatPhongs.SingleOrDefaultAsync(x => x.IdPhieuDatPhong == idPhieuDatPhong);
            if (check != null)
            {
                check.Status = TrangThaiPhieuDatPhong.Successful;
                var phong = await _db.Phongs.SingleOrDefaultAsync(x => x.IdPhong == check.IdPhong);
                phong.StatusPhong = StatusPhong.NotEmpty;
                _db.Entry(phong).State = EntityState.Modified;
                _db.Entry(check).State = EntityState.Modified;
                _db.SaveChanges();

            }
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
                             SoDienThoai = PhieuDatPhong.SoDienThoai,
                             GiaPhong = Phong.GiaPhong.ToString(),
                             trangthai =PhieuDatPhong.Status.ToString()
                         }).Skip((pageNumber-1)*pageSize).Take(pageSize);
            return await  query.ToListAsync();
        }
      
        public async Task<List<PhieuDatPhong>> GetByAllPhieuDatPhong(ViewSeach model)
        {
            var query = _db.PhieuDatPhongs.AsQueryable();
            
            if (!string.IsNullOrWhiteSpace(model.Name))
            {
                query = query.Where(p => p.TenNguoiDat.Contains(model.Name));
            }

            if (!string.IsNullOrWhiteSpace(model.Sodienthoai))
            {
                query = query.Where(p => p.SoDienThoai.Contains(model.Sodienthoai));
            }

            if (!string.IsNullOrWhiteSpace(model.tenPhong))
            {
                query = query.Where(p => p.TenPhong.Contains(model.tenPhong));
            }
           
            return await query.ToListAsync();
        }

        public async Task TraDatPhong(int idPhieuDatPhong)
        {
            var check = await _db.PhieuDatPhongs.SingleOrDefaultAsync(x => x.IdPhieuDatPhong == idPhieuDatPhong);
            if (check != null)
            {
                check.Status = TrangThaiPhieuDatPhong.Checked_Out;
                var phong = await _db.Phongs.SingleOrDefaultAsync(x => x.IdPhong == check.IdPhong);
                phong.StatusPhong = StatusPhong.Empty;
                _db.Entry(phong).State = EntityState.Modified;
                _db.Entry(check).State = EntityState.Modified;
                _db.SaveChanges();
            }
        }
    }
}

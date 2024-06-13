using APISolution.Database.DatabaseContext;
using APISolution.Database.Entity;
using APISoluton.Application.Interface.PhieuDatPhong.Commands;
using APISoluton.Application.Interface.PhieuDatPhong.Queries;
using APISoluton.Application.ViewModel.PhieuDatPhongView;
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

        public async Task<PhieuDatPhongVM> DatPhong(PhieuDatPhongVM mode)
        {
            var map = _mapper.Map<PhieuDatPhong>(mode);
            var check = await _db.PhieuDatPhongs.SingleOrDefaultAsync(x=>x.IdPhieuDatPhong == map.IdPhieuDatPhong);
            if (check != null)
            {
                return null;
            }
            else
            {
                _db.PhieuDatPhongs.Add(map);
                await _db.SaveChangesAsync();
                return new PhieuDatPhongVM
                {
                    IdPhong = map.IdPhong,
                    TenPhong = map.TenPhong,
                    TenNguoiDat = map.TenNguoiDat,
                    SoDienThoai = map.SoDienThoai,
                    GiaPhong = map.GiaPhong,
                    Status = map.Status,
                };

            }
        }

        public async Task<List<PhieuDatPhongVM>> GetAllPhieuDatPhong()
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
                         }
                         );
            return await  query.ToListAsync();
        }

        public Task<PhieuDatPhongVM> GetByIdPhieuDatPhong(int id)
        {
            throw new NotImplementedException();
        }
    }
}

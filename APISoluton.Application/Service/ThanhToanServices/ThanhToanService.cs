using APISolution.Database.DatabaseContext;
using APISolution.Database.Entity;
using APISolution.Database.Enum;
using APISolution.Database.Stored_Procedure;
using APISolution.Database.ViewModel.ThanhToanView;
using APISoluton.Application.Interface.IThanhToan.Commands;
using APISoluton.Application.Interface.IThanhToan.Queries;
using APISoluton.Application.Service.CacheServices;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Service.ThanhToanServices
{
    public class ThanhToanService : IThanhToanCommand ,IThanhToanQueries
    {
        private readonly IMapper _mapper;
        private readonly DdConnect _db;
        private readonly ProcedureThanhToan _procedure;
        static string _key = "thanhToan";
        private readonly CacheServices.CacheServices _cacheServices;
        public ThanhToanService(IMapper mapper, DdConnect db,ProcedureThanhToan procedure,CacheServices.CacheServices cacheServices)
        {
            _mapper = mapper;
            _db = db;
            _procedure = procedure;
            _cacheServices = cacheServices;
        }

        public async Task DuyetThanhToan(int idThanhToan)
        {
            await _procedure.DuyetThanhToanStored(idThanhToan);
            _cacheServices.Remove(_key);
        }

        public async Task<List<ThanhToanVM>> GetListThanhToan(int pageNumber, int pageSize)
        {
            var check = await _db.ThanhToans.AsNoTracking().ToListAsync();
            var query =  check.Select(x=>new ThanhToanVM
            {
           //  IdPhieuDatPhong =  x.PhieuThanhToan_DatPh
                TenPhong = x.TenPhong ,
                TenKhachHang = x.TenKhachHang ,
                TongThanhTien = x.TongThanhTien ,
                TrangThaiThanhToan = x.TrangThaiThanhToan.ToString()
            }).Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
            _key= $"thanhToan_{pageNumber}_{pageSize}";
            _cacheServices.SetList(_key, query.ToList(), TimeSpan.FromMinutes(30));
            return  query.ToList();
        }

        public async Task<List<ThanhToanVM>> ThanhToan(ThanhToanAddVM model)
        {
           var a = await _procedure.CreatThanhToanStored(model);
            var thanhtienDv = _db.PhieuDichVus.Where(x => x.IdPhieuDatPhong == model.idPhieuDatPhong).Sum(x => x.ThanhTien);
            var query = (from ThanhToan_PhieuDatPhong in _db.ThanhToan_PhieuDatPhong
                         join PhieuDichVu in _db.PhieuDichVus on ThanhToan_PhieuDatPhong.IdPhieuDatPhong equals PhieuDichVu.IdPhieuDichVu
                         join ThanhToan in _db.ThanhToans on ThanhToan_PhieuDatPhong.IdThanhToan equals ThanhToan.IdThanhToan
                         select new ThanhToanVM
                         {
                             TenKhachHang = PhieuDichVu.TenKhachHang,
                             TenPhong = ThanhToan_PhieuDatPhong.PhieuDatPhong.TenPhong,
                             IdPhieuDatPhong = ThanhToan_PhieuDatPhong.IdPhieuDatPhong,
                             TongThanhTien = thanhtienDv + ThanhToan_PhieuDatPhong.PhieuDatPhong.GiaPhong,
                             TrangThaiThanhToan = ThanhToan.TrangThaiThanhToan.ToString()
                         }).Where(x => x.IdPhieuDatPhong == model.idPhieuDatPhong).ToListAsync();
            _cacheServices.Remove(_key);
            return await query;
        }
    }
}

using APISolution.Database.DatabaseContext;
using APISolution.Database.Entity;
using APISolution.Database.Stored_Procedure;
using APISolution.Database.ViewModel.PhieuDatDichVuView;
using APISolution.Database.ViewModel.PhieuDatDichVuView.PhieuDatDichVuViewShow;
using APISoluton.Application.Interface.IPhieuDatDichVu.Commands;
using APISoluton.Application.Interface.IPhieuDatDichVu.Queries;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Service.PhieuDatDichVuServices
{
    public class PhieuDatDichVuService : IPhieuDatDichVuCommand,IPhieuDatDichVuQueries
    {
        private readonly DdConnect _db;
        private readonly IMapper _mapper;
       static string _keyPhieuDatDV = "phieuDatDV";
        private readonly CacheServices.CacheServices _cacheServices;
        private readonly ProcedurePhieuDatDichVu _procedure;
        public PhieuDatDichVuService(DdConnect db, IMapper mapper, CacheServices.CacheServices cacheServices,ProcedurePhieuDatDichVu procedure)
        {
            _db = db;
            _mapper = mapper;
            _cacheServices = cacheServices;
            _procedure = procedure;
        }

        public async Task<AddPhieuDatDichVuView> AddPhieuDatDichVu(AddPhieuDatDichVuView model)
        {
            #region Chỉnh sủa 
            //var map = _mapper.Map<PhieuDichVu>(model);
            //   var checkphieuDichVu= await _db.PhieuDichVus.SingleOrDefaultAsync(x=>x.IdPhieuDichVu == map.IdPhieuDichVu);
            //   if (checkphieuDichVu != null)
            //   {
            //       return null;
            //   }
            //   else
            //   {
            //       var checkdv = _db.DichVus.FirstOrDefault(x => x.IdDichVu == map.IdDichVu);
            //       var checkPDP = await _db.PhieuDatPhongs.SingleOrDefaultAsync(x => x.IdPhieuDatPhong == map.IdPhieuDatPhong);
            //       if (checkPDP != null && checkdv != null)
            //       {
            //           var pdv = new PhieuDichVu();
            //           pdv.TenDichVu = checkdv.NameDichVu;
            //           pdv.IdDichVu = checkdv.IdDichVu;
            //           pdv.IdPhieuDatPhong = checkPDP.IdPhieuDatPhong;
            //           pdv.TenPhong = checkPDP.TenPhong;
            //           pdv.Gia = checkdv.Gia;
            //           pdv.SoLuong = model.SoLuong;
            //           pdv.ThanhTien = (double)pdv.Gia*model.SoLuong;
            //           pdv.TenKhachHang = checkPDP.TenNguoiDat;
            //           _db.PhieuDichVus.Add(pdv);
            //           checkdv.SoLuong -= model.SoLuong;
            //           _db.Entry(checkdv).State = EntityState.Modified;
            //           _db.SaveChanges();
            //           return model;
            //       }
            //   }
            //   return null;
            #endregion
           var dichvu= await _procedure.AddPhieuDatDichVuProcedure(model);
            _cacheServices.Remove(_keyPhieuDatDV);
            return dichvu;
        }

            public async Task UpdatePhieuDatDichVu(int id, AddPhieuDatDichVuView model)
        {
            if (id == 0)
            {
                await Task.FromException<int>(new InvalidOperationException("Lỗi "));
            }
            else
            {
                await _procedure.UpdatePhieuDatDichVuProcedure(id, model);
                _cacheServices.Remove(_keyPhieuDatDV);
            }
        }

        public async Task DeletePhieuDatDichVu(int id)
        {
            var check =  _db.PhieuDichVus.Where(x => x.IdPhieuDichVu == id).FirstOrDefault();
            if (check != null)
            {
                await _procedure.DeletePhieuDatDichVuProcedure(id);
                _cacheServices.Remove(_keyPhieuDatDV);
            }
        }

        public async Task<List<PhieuDatDichVuVM>> GetAllPhieuDichVu(int pageNumber, int pageSize)
        {
         
            var query = (from PhieuDichVu in _db.PhieuDichVus
                         join PhieuDatPhong in _db.PhieuDatPhongs on PhieuDichVu.IdPhieuDatPhong equals PhieuDatPhong.IdPhieuDatPhong
                         join DichVu in _db.DichVus on PhieuDichVu.IdDichVu equals DichVu.IdDichVu
                         select new PhieuDatDichVuVM
                         {
                             TenKhachHang = PhieuDatPhong.TenNguoiDat,
                             IdPhieuDatPhong = PhieuDatPhong.IdPhieuDatPhong,
                             IdDichVu = DichVu.IdDichVu,
                             SoLuong =  PhieuDichVu.SoLuong,
                             Gia = DichVu.Gia ,
                             IdPhong = PhieuDatPhong.IdPhong,
                             TenDichVu = DichVu.NameDichVu,
                             TenPhong = PhieuDatPhong.TenPhong,
                             ThanhTien = PhieuDichVu.ThanhTien,
                             NgayDatDichVu = PhieuDichVu.NgayDatDichVu.ToString("dd/MM/yyyy",CultureInfo.InvariantCulture)
                         }
                         ).Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
            _cacheServices.SetList(_keyPhieuDatDV, query.ToList(), TimeSpan.FromMinutes(30));
            return  query;
        }

        public async Task<List<PhieuDichVu>> GetByAllPhieuDichVu(SearchViewPhieuDatDV model)
        {
            var query =  _db.PhieuDichVus.AsQueryable();

            if (!string.IsNullOrWhiteSpace(model.TenPhong))
            {
                query = query.Where(p => p.TenPhong.Contains(model.TenPhong));
            }
            if (!string.IsNullOrWhiteSpace(model.TenNguoiDat))
            {
                query = query.Where(p => p.TenKhachHang.Contains(model.TenNguoiDat));
            }
            _cacheServices.Remove(_keyPhieuDatDV);
            return  await query.ToListAsync();
        }
    }
}

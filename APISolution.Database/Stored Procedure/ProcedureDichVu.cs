using APISolution.Database.DatabaseContext;
using APISolution.Database.Entity;
using APISoluton.Database.ViewModel.DichVuView;
using APISoluton.Database.ViewModel.LoaiPhongView;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISolution.Database.Stored_Procedure
{
    public class ProcedureDichVu
    {
        private readonly DdConnect _db;
        public ProcedureDichVu(DdConnect db)
        {
            _db = db;
        }
        public async Task UpdateDichVuStored(int id, DichVuVM model)
        {
            await _db.Database.ExecuteSqlInterpolatedAsync(
               $"EXEC UpdateDichVu @Id={id},@NameDichVu = {model.NameDichVu},@SoLuong= {model.SoLuong},@Gia ={model.Gia}");
        }
        public async Task<DichVuVM> CreatDichVuStored(DichVuVM model)
        {
            await _db.Database.ExecuteSqlInterpolatedAsync($"EXEC AddDichVu @NameDichVu = {model.NameDichVu},@SoLuong= {model.SoLuong},@Gia ={model.Gia}");
            return model;
        }
        public async Task<DichVu> GetByIdDichVu(int id)
        {
            var resual = _db.DichVus.FromSql($"EXECUTE  dbo.GetByIdDichVu @Id = {id}").AsEnumerable().SingleOrDefault();
            return resual;
        }

        public async Task DeleteDichVu(int id)
        {
            await _db.Database.ExecuteSqlInterpolatedAsync($"EXEC DeleteDichVu @Id = {id}");
        }
    }
}

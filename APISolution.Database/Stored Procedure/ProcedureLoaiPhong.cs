using APISolution.Database.DatabaseContext;
using APISolution.Database.Entity;
using APISoluton.Database.ViewModel.LoaiPhongView;
using APISoluton.Database.ViewModel.PhongView;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISolution.Database.Stored_Procedure
{
    public class ProcedureLoaiPhong
    {
        private readonly DdConnect _db;
        public ProcedureLoaiPhong(DdConnect db)
        {
            _db = db;
        }
        public async Task UpdateLoaiPhongStored(int id, LoaiPhongVM model)
        {
            await _db.Database.ExecuteSqlInterpolatedAsync(
               $"EXEC UpdateLoaiPhong @Id={id},@Name = {model.Name},@Mota = {model.MoTa}");
        }
        public async Task<LoaiPhongVM> CreatLoaiPhongStored(LoaiPhongVM model)
        {
            await _db.Database.ExecuteSqlInterpolatedAsync($"EXEC AddLoaiPhong @Name = {model.Name},@Mota = {model.MoTa}");
            return model;
        }
        public async Task<LoaiPhong> GetByIdLoaiPhong(int id)
        {
            var resual = _db.Loais.FromSql($"EXECUTE  dbo.GetByIdLoaiPhong @Id = {id}").AsEnumerable().SingleOrDefault();
            return resual;
        }

        public async Task DeleteLoaiPhong(int id)
        {
            await _db.Database.ExecuteSqlInterpolatedAsync($"EXEC DeleteLoaiPhong @Id = {id}");
        }
    }
}

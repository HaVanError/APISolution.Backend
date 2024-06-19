using APISolution.Database.DatabaseContext;
using APISolution.Database.Entity;
using APISoluton.Database.ViewModel.PhongView;
using APISoluton.Database.ViewModel.RoleView;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace APISolution.Database.Stored_Procedure
{
    public class ProcedurePhong
    {
        private readonly DdConnect _db;
        public  ProcedurePhong(DdConnect Db)
        {
            _db = Db;
        }
        public async Task UpdatePhongStored(int id, PhongVM model)
        {
            await _db.Database.ExecuteSqlInterpolatedAsync(
               $"EXEC UpdatePhong @Id={id},@Name = {model.Name},@Mota = {model.Describe},@IdLoaiPhong = {model.IdLoaiPhong},@status = {model.StatusPhong},@GiaPhong={model.GiaPhong}");
        }
        public async Task<PhongVM> CreatPhongStored(PhongVM model)
        {
             await _db.Database.ExecuteSqlInterpolatedAsync($"EXEC AddPhong @Name = {model.Name},@Mota = {model.Describe},@IdLoaiPhong = {model.IdLoaiPhong},@status = {model.StatusPhong},@GiaPhong={model.GiaPhong}");
            return model;
        }
        public async Task<Phong> GetByIdPhong(int id)
        {
            var resual =  _db.Phongs.FromSql($"EXECUTE  dbo.GetByIdPhong @Id = {id}").AsEnumerable().SingleOrDefault();
            return resual;
        }

        public async Task<List<Phong>> GetAllPhongs(int pageNumber, int pageSize)
        {
            var pageNumberParam = new SqlParameter("@PageNumber", pageNumber);
            var pageSizeParam = new SqlParameter("@PageSize", pageSize);
            var roles = await _db.Phongs.FromSqlRaw($"EXECUTE dbo.GetAllPhong @PageNumber, @PageSize", pageNumberParam, pageSizeParam).ToListAsync();
            return roles;
        }
        public async Task DeletePhong(int id)
        {
            await _db.Database.ExecuteSqlInterpolatedAsync($"EXEC DeletePhong @Id = {id}");
        }
    }
}

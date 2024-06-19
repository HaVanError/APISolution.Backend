using APISolution.Database.DatabaseContext;
using APISolution.Database.Entity;
using APISoluton.Database.ViewModel.RoleView;
using APISoluton.Database.ViewModel.UserView;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Database.Stored_Procedure
{
    public class ProcedureRole
    {
        private readonly DdConnect _db;
        public ProcedureRole(DdConnect db)
        {
            _db = db;
        }
        public async Task UpdateRoleStored(int id, RoleVM model)
        {
            await _db.Database.ExecuteSqlInterpolatedAsync(
               $"EXEC UpdateQuyen @IdQuyen={id}, @NameRole = {model.NameRole}, @MoTa = {model.Description}");
        }
        public async Task CreatRoleStored(RoleVM model)
        {
         await _db.Database.ExecuteSqlInterpolatedAsync($"EXEC AddQuyen @NameRole = {model.NameRole},@MoTa = {model.Description}");
             
        }
    
        public async Task<List<Role>> GetAllRoles(int pageNumber, int pageSize)
        {
            var pageNumberParam = new SqlParameter("@PageNumber", pageNumber);
            var pageSizeParam = new SqlParameter("@PageSize", pageSize);
            var roles = await _db.Roles.FromSqlRaw($"EXECUTE dbo.GetAllQuyen @PageNumber, @PageSize", pageNumberParam, pageSizeParam).ToListAsync();

            return roles;

        }
        public async Task DeleteRole(int id)
        {
            await _db.Database.ExecuteSqlInterpolatedAsync($"EXEC DeleteQuyen @Id = {id}");
        }
    }
}

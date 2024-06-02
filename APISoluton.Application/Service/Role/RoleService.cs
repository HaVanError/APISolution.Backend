
using APISolution.Database.DatabaseContext;
using APISolution.Database.Entity;
using APISoluton.Application.Interface.Role.Commands;
using APISoluton.Application.Interface.Role.Queries;
using APISoluton.Application.ViewModel;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Service.Role
{
    public class RoleService : IRole, IRoles
    {
        private readonly DdConnect _db;
        public RoleService(DdConnect db)
        {
            _db = db;
        }
        public string Add(RoleVM vn)
        {
            var reo = new APISolution.Database.Entity.Role();
            reo.NameRole = vn.NameRole;
            reo.MoTa = vn.Description;
            _db.Roles.Add(reo);
            _db.SaveChanges();
            return "Thanh cong";
        }

        public string Delete(int id)
        {
           var check = _db.Roles.Where(x=>x.idRole == id).FirstOrDefault();
            if (check!=null)
            {
                _db.Roles.Remove(check);
                _db.SaveChanges();
                return "Xóa thành công Quyền + " +check.NameRole + "";
            }
            return "Không tìm thấy";
        }
        public List<RoleVM> GetAllRoles()
        {
            var ds = _db.Roles.ToList();
            var kq = ds.Select(x => new RoleVM
            {
                NameRole = x.NameRole,
                Description = x.MoTa
            }).ToList();
            return kq;
        }
        public RoleVM? GetRoleByName(string name)
        {
            var check = _db.Roles.SingleOrDefault(x => x.NameRole == name);
            if (check != null)
            {
                return new RoleVM
                {
                    NameRole = check.NameRole,
                    Description = check.MoTa
                };
            }
            else
            {
                return null;
            }
        }
        public string Update(RoleVM vn, int id)
        {
            var check = _db.Roles.SingleOrDefault(X=>X.idRole == id);
            if (check != null)
            {
                check.NameRole = vn.NameRole;
                check.MoTa = vn.Description;
                _db.Entry(check).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _db.SaveChanges();
                return "Cập nhật thành công";
            }
            return "Cập nhất không thành công ";
        }
    }
}

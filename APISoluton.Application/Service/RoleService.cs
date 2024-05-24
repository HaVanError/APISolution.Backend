
using APISolution.Database.DatabaseContext;
using APISolution.Database.Entity;
using APISoluton.Application.Conmon;
using APISoluton.Application.IService;
using APISoluton.Application.ViewModel;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Service
{
    public class RoleService : IRole,IRoles
    {
        private readonly DdConnect _db;
        public RoleService(DdConnect db)
        {
            _db = db;
        }
        public string Add(RoleVM vn)
        {
            var reo = new Role();
            reo.NameRole = vn.NameRole;
            reo.MoTa = vn.Description;
            _db.Roles.Add(reo);
            _db.SaveChanges();
            return "Thanh cong";
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
           var check = _db.Roles.SingleOrDefault(x=>x.NameRole == name);
            if(check != null)
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
    }
}

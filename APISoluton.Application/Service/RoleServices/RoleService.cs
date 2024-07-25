using APISolution.Database.DatabaseContext;
using APISolution.Database.Entity;
using APISoluton.Application.Interface.IRole.Commands;
using APISoluton.Application.Interface.IRole.Queries;
using APISoluton.Application.Service.CacheServices;
using APISoluton.Database.Stored_Procedure;
using APISoluton.Database.ViewModel.RoleView;
using APISoluton.Database.ViewModel.UserView.UserViewShow;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Service.RoleServices
{
    public class RoleService : IRoleCommand, IRolesQueries
    {
        private readonly DdConnect _db;
        private readonly ProcedureRole _role;
        private readonly IMapper _mapper;

        public RoleService(DdConnect db ,ProcedureRole role,IMapper mapper)
        {
            _db = db;
            _role=role;
            _mapper = mapper;
        }
        public async Task<RoleVM> Add(RoleVM vn)
        {
           await _role.CreatRoleStored(vn);
            return vn;
        }

        public async Task Delete(int id)
        {
            var key = "Role";
            var check = await _db.Roles.FindAsync(id);
            if (check != null)
            {
                await _role.DeleteRole(id);
            }
        }
        public async Task<List<RoleVM>> GetAllRoless(int pagenumber ,int pagesize)
        {
            var list = await _role.GetAllRoles(pagenumber, pagesize);
            var query = list.Select(x => new RoleVM
            {
                NameRole = x.NameRole,
                Description = x.MoTa
            }).ToList();
            return query;
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
        public async Task<RoleVM> Update(RoleVM vn, int id)
        {
          var check = _db.Roles.SingleOrDefault(X=>X.IdRole == id);
        
            if (check !=null)
            {
                await _role.UpdateRoleStored(id, vn);
                return vn;
            }
            return null;
        }

        
    }
}

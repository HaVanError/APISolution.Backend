using APISolution.Database.Entity;
using APISoluton.Database.ViewModel.RoleView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Interface.IRole.Queries
{
    public interface IRolesQueries
    {
        Task<List<RoleVM>> GetAllRoless(int pagenumber, int pagesize);
        RoleVM GetRoleByName(string name);
    }
}

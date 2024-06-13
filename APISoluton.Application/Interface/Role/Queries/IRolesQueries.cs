using APISoluton.Application.ViewModel.RoleView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Interface.Role.Queries
{
    public interface IRolesQueries
    {
        List<RoleVM> GetAllRoles();
        RoleVM GetRoleByName(string name);
    }
}

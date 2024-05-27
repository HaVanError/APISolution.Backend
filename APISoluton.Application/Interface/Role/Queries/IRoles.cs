using APISoluton.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Interface.Role.Queries
{
    public interface IRoles
    {
        List<RoleVM> GetAllRoles();
        RoleVM GetRoleByName(string name);
    }
}

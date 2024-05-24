using APISoluton.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Conmon
{
    public interface IRoles
    {
        List<RoleVM> GetAllRoles();
        RoleVM GetRoleByName(string name);
    }
}

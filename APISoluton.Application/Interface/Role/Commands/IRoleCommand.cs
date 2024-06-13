using APISoluton.Application.ViewModel.RoleView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Interface.Role.Commands
{
    public interface IRoleCommand
    {
        string Add(RoleVM vn);
        string Delete(int id );
        string Update(RoleVM vn,int id);

    }
}

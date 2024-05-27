using APISoluton.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Interface.Role.Commands
{
    public interface IRole
    {
        string Add(RoleVM vn);
        string Delete(int id );
        string Update(RoleVM vn,int id);

    }
}

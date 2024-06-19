using APISoluton.Database.ViewModel.RoleView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Interface.IRole.Commands
{
    public interface IRoleCommand
    {
        Task<RoleVM> Add(RoleVM vn);
        Task Delete(int id );
        Task<RoleVM>  Update(RoleVM vn,int id);

    }
}

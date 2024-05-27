using APISoluton.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Interface.User.Queries
{
    public interface IUsers
    {
        Task<List<UserVMShowAll>> GetListUsers();
        // Task<UserVM> GetUserById(int id);
    }
}

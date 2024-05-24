using APISoluton.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Conmon
{
    public interface IUsers 
    {
        Task<List<UserVMShowAll>> GetListUsers();
       // Task<UserVM> GetUserById(int id);
    }
}

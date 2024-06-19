
using APISoluton.Database.ViewModel.UserView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Interface.IUsers.Commands
{
    public interface IUserCommaind
    {
        Task<UserVM> CreatUser(UserVM userVM);
        Task DeleteUser(int id );
        Task<int> UpdateUser(int id, UserVM userVM);
    }
}

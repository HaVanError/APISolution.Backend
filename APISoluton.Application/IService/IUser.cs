using APISoluton.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.IService
{
    public interface IUser
    {
        Task<string> CreatUser(UserVM userVM);
       // Task<string> UpdateUser(UserVM userVM);
       // Task<string> DeleteUser(UserVM userVM);
    }
}

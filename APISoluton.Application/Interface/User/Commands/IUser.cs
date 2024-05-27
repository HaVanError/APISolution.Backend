using APISoluton.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Interface.User.Commands
{
    public interface IUser
    {
        Task<string> CreatUser(UserVM userVM);
    }
}

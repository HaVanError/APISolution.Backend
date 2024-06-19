using APISolution.Database.Entity;
using APISoluton.Database.ViewModel.UserView;
using APISoluton.Database.ViewModel.UserView.UserViewShow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Interface.IUsers.Queries
{
    public interface IUsersQueries
    {
        Task<List<UserVMShowAll>> GetListUsers(int pageNumber, int pageSize);
        Task<User> GetUserByName(string name);
      
    }
}

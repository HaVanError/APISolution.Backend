using APISolution.Database.Entity;
using APISoluton.Application.ViewModel.UserView.UserViewShow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Interface.IUsers.Queries
{
    public interface IUsersQueries
    {
        Task<List<User>> GetListUsers();
        Task<User> GetUserByName(string name);
      
    }
}

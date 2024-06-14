using APISolution.Application.Stored_Procedure;
using APISolution.Database.DatabaseContext;
using APISolution.Database.Entity;
using APISoluton.Application.Interface.IUsers.Commands;
using APISoluton.Application.Interface.IUsers.Queries;
using APISoluton.Application.Service.CacheServices;
using APISoluton.Application.ViewModel.UserView;
using APISoluton.Application.ViewModel.UserView.UserViewShow;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;


namespace APISoluton.Application.Service.UserServices
{
    public class UserService : IUserCommaind, IUsersQueries
    {
        private readonly IMapper _mapper;
        private readonly DdConnect _db;
        private readonly CacheServices.CacheServices _cacheServices;
        private readonly Procedure _procedure;
        public UserService(IMapper mapper, DdConnect db , CacheServices.CacheServices cacheServices, Procedure procedure) 
        {
            _mapper = mapper;
            _db = db;
            _cacheServices = cacheServices;
            _procedure = procedure;
        }
        public async Task<UserVM> CreatUser(UserVM userVM)
        {
             await _procedure.CreatUserStored(userVM);
            return userVM;
      
          
        }
        public async Task DeleteUser(int id)
        {
            var checkUser = await _db.Users.FindAsync(id);
            if (checkUser != null)
            {
                await _procedure.DeleteUserAsync(id);
                string key = "User";
                _cacheServices.Refresh(key, checkUser, TimeSpan.FromMinutes(30));
            }
        }
        public async Task<List<User>> GetListUsers(int pageNumber, int pageSize)
        {
            //var data =  from User in _db.Users
            //                 join Role in _db.Roles on User.IdRole equals Role.IdRole
            //                 select new UserVMShowAll
            //                 {
            //                     Name = User.Name,
            //                     Email = User.Email,
            //                     Password = User.Password,
            //                     Address = User.Address,
            //                     City = User.City,
            //                     Role = Role.NameRole
            //                 };
            var list= await _procedure.GetAllUsers( pageNumber,  pageSize);
            var key = "User";
            _cacheServices.SetList(key, list, TimeSpan.FromMinutes(30));
            return list;
        }
        public async Task<User> GetUserByName(string name)
        {
            var resul = await _procedure.GetUserStoredByName(name);
            string key = "User";
            _cacheServices.Refresh(key, resul.ToList(), TimeSpan.FromMinutes(30));
            return resul.FirstOrDefault();
        }
        public async Task<int> UpdateUser(int id, UserVM userVM)
        {
            var checkuser = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (checkuser != null)
           {
            await _procedure.UpdateUserStored(id, userVM);
                string key = "User";
                _cacheServices.Refresh(key, checkuser, TimeSpan.FromMinutes(30));
                return id;
            }
            return 0;
        }
    }
}

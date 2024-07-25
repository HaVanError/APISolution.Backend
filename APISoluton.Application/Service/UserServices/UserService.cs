using APISolution.Database.DatabaseContext;
using APISolution.Database.Entity;
using APISolution.Database.Stored_Procedure;
using APISoluton.Application.Interface.IUsers.Commands;
using APISoluton.Application.Interface.IUsers.Queries;
using APISoluton.Application.Service.CacheServices;

using APISoluton.Database.ViewModel.UserView;
using APISoluton.Database.ViewModel.UserView.UserViewShow;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace APISoluton.Application.Service.UserServices
{
    public class UserService : IUserCommaind, IUsersQueries
    {
        private readonly IMapper _mapper;
        private readonly DdConnect _db;
        private readonly ProcedureUser _procedure;
        public UserService(IMapper mapper, DdConnect db , ProcedureUser procedure) 
        {
            _mapper = mapper;
            _db = db;
          
            _procedure = procedure;
        }
        public async Task<UserVM> CreatUser(UserVM userVM)
        {
            var map = _mapper.Map<User>(userVM);
            await _procedure.CreatUserStored(userVM);
            return userVM;
        }
        public async Task DeleteUser(int id)
        {
            var checkUser = await _db.Users.FindAsync(id);
            if (checkUser != null)
            {
                await _procedure.DeleteUserAsync(id);
            }
        }
        public async Task<List<UserVMShowAll>> GetListUsers(int pageNumber, int pageSize)
        {
            var list= await _procedure.GetAllUsers( pageNumber,  pageSize);
            var query = (from User in list.ToList()
                        join Role in _db.Roles on User.IdRole equals Role.IdRole
                        select new UserVMShowAll
                        {
                            Address = User.Address,
                            City = User.City,
                            Email = User.Email,
                            Name = User.Name,
                            Password = User.Password,
                            Role = Role.NameRole,
                        }).ToList();
            return query;
        }
        public async Task<User> GetUserByName(string name)
        {
            var resul = await _procedure.GetUserStoredByName(name);
            return resul.FirstOrDefault();
        }
        public async Task<int> UpdateUser(int id, UserVM userVM)
        {
            var checkuser = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (checkuser != null)
            {
                await _procedure.UpdateUserStored(id, userVM);
                var map = _mapper.Map<User>(userVM);
                return id;
            }
            return 0;

        }
       
    
    }
}

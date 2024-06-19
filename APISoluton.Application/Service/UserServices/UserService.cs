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
       static string _keyUser = "user";
        private readonly CacheServices.CacheServices _cacheServices;
        private readonly ProcedureUser _procedure;
        public UserService(IMapper mapper, DdConnect db , CacheServices.CacheServices cacheServices, ProcedureUser procedure) 
        {
            _mapper = mapper;
            _db = db;
            _cacheServices = cacheServices;
            _procedure = procedure;
        }
        public async Task<UserVM> CreatUser(UserVM userVM)
        {
            var map = _mapper.Map<User>(userVM);
            await _procedure.CreatUserStored(userVM);
            _cacheServices.Remove(_keyUser);
            return userVM;
        }
        public async Task DeleteUser(int id)
        {
            var checkUser = await _db.Users.FindAsync(id);
            if (checkUser != null)
            {
                await _procedure.DeleteUserAsync(id);
                _cacheServices.Remove(_keyUser);
            }
        }
        public async Task<List<UserVMShowAll>> GetListUsers(int pageNumber, int pageSize)
        {

            //  cacheKey += $"_{pageNumber}_{pageSize}";
            _keyUser = $"user_{pageNumber}_{pageSize}";
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
            _cacheServices.SetList(_keyUser, query,TimeSpan.FromMinutes(30));
            return query;
        }
        public async Task<User> GetUserByName(string name)
        {
            var resul = await _procedure.GetUserStoredByName(name);
            _cacheServices.Remove(_keyUser);
            return resul.FirstOrDefault();
        }
        public async Task<int> UpdateUser(int id, UserVM userVM)
        {
            var checkuser = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (checkuser != null)
            {
                await _procedure.UpdateUserStored(id, userVM);
                var map = _mapper.Map<User>(userVM);
                _cacheServices.Remove(_keyUser);
                return id;
            }
            return 0;

        }
       
    
    }
}

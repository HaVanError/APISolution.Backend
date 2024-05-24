using APISolution.Database.DatabaseContext;
using APISolution.Database.Entity;
using APISoluton.Application.Conmon;
using APISoluton.Application.IService;
using APISoluton.Application.ViewModel;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Service
{
    public class UserService : IUser ,IUsers
    {
        private readonly IMapper _mapper;
        private readonly DdConnect _db;
        public UserService(IMapper mapper, DdConnect db)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<string> CreatUser(UserVM userVM)
        {
            var map = _mapper.Map<User>(userVM);
          await  _db.Users.AddAsync(map);
          await  _db.SaveChangesAsync();
            return "thanh cong";
        }

        public async Task<List<UserVMShowAll>> GetListUsers()
        {
            //var ds = await _db.Users.ToListAsync();
            //var check = await _db.Roles.SingleOrDefaultAsync();
            //var resual = ds.Select(x=>new UserVMShowAll
            //{
            //   Name  = x.Name,
            //   Address = x.Address,
            //   City = x.City,
            //   Email = x.Email,
            //   Password= x.Password,
            //   Role = ds.Where(x=>x.IdRole ==check.idRole).Select(x=>x.Role.NameRole).SingleOrDefault()
            //}).ToList();
            //return resual.ToList();
            var data = from User in _db.Users
                       join Role in _db.Roles on User.IdRole equals Role.idRole
                       select new UserVMShowAll
                       {
                           Name = User.Name,
                           Email = User.Email,
                           Password = User.Password,
                           Address = User.Address,
                           City = User.City,
                           Role = Role.NameRole

                       };
            return data.ToList();
        }
    }

   
}

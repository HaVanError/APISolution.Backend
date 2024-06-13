﻿using APISolution.Database.DatabaseContext;
using APISolution.Database.Entity;
using APISoluton.Application.ViewModel.UserView;
using APISoluton.Application.ViewModel.UserView.UserViewShow;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace APISolution.Application.Stored_Procedure
{
    public class Procedure
    {
        private readonly DdConnect _db;
        public Procedure(DdConnect db) { 
        _db = db;
    }
        public async Task UpdateUserStored(int id,UserVM model)
        {
            await _db.Database.ExecuteSqlInterpolatedAsync(
               $"EXEC UpdateUser @Id={id}, @Email = {model.Email}, @Password = {model.Password}, @Name = {model.Name}, @Address = {model.Address}, @City = {model.City}, @IdRole = {model.IdRole}");
        }
        public  async Task CreatUserStored(UserVM model)
        {
            await _db.Database.ExecuteSqlInterpolatedAsync($"EXEC AddUser @Name = {model.Name},@Email = {model.Email},@Password = {model.Password},@Address = {model.Address},@City = {model.City},@IdRole = {model.IdRole}");
        
        }
        public async Task<List<User>> GetUserStoredByName(string name)
        {
            
            var parameter = new SqlParameter("@Name", name);
            var users = await _db.Users
                .FromSqlRaw($"EXECUTE GetUserByName @Name", parameter)
                .ToListAsync();

            return users;
        }
        public async Task<List<User>> GetAllUsers()
        {
            var users=  await _db.Users.FromSqlRaw($"EXECUTE GetAllUser").ToListAsync();
            return users;


        }
        public async Task DeleteUserAsync(int id)
        {
            await _db.Database.ExecuteSqlInterpolatedAsync($"EXEC DeleteUser @Id = {id}");
        }
    }
}

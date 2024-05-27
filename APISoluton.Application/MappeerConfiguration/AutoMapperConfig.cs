using APISolution.Database.Entity;
using APISoluton.Application.ViewModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.MappeerConfiguration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<APISolution.Database.Entity.User, UserVM>();
            CreateMap<UserVM, APISolution.Database.Entity.User>();
            CreateMap<Role, RoleVM>();
            CreateMap<RoleVM, Role>();
        }
    }
}

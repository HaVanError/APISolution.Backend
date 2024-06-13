using APISolution.Database.Entity;
using APISoluton.Application.ViewModel.DichVuView;
using APISoluton.Application.ViewModel.LoaiPhongView;
using APISoluton.Application.ViewModel.PhieuDatPhongView;
using APISoluton.Application.ViewModel.PhongView;
using APISoluton.Application.ViewModel.PhongView.PhongViewShowClient;
using APISoluton.Application.ViewModel.RoleView;
using APISoluton.Application.ViewModel.UserView;
using APISoluton.Application.ViewModel.UserView.UserViewShow;
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
            CreateMap<User, UserVM>();
            CreateMap<UserVM, User>();
            CreateMap<User, UserVMShowAll>();
            CreateMap<UserVMShowAll, User>();
            CreateMap<Role, RoleVM>();
            CreateMap<RoleVM, Role>();
            CreateMap<Phong, PhongVM>();
            CreateMap<PhongVM, Phong>();
            CreateMap<PhongVMShow, Phong>();
            CreateMap<Phong, PhongVMShow>();
            CreateMap<LoaiPhong, LoaiPhongVM>();
            CreateMap<LoaiPhongVM, LoaiPhong>();
            CreateMap<PhieuDatPhong, PhieuDatPhongVM>();
            CreateMap<PhieuDatPhongVM, PhieuDatPhong>();
            CreateMap<DichVuVM, DichVu>();
            CreateMap<DichVu, DichVuVM>();
        }
    }
}

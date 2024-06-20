using APISolution.Database.Entity;
using APISolution.Database.ViewModel.PhieuDatPhongView.PhieuDatPhongViewShow;
using APISoluton.Database.ViewModel.DichVuView;
using APISoluton.Database.ViewModel.LoaiPhongView;
using APISoluton.Database.ViewModel.PhieuDatPhongView;
using APISoluton.Database.ViewModel.PhongView;
using APISoluton.Database.ViewModel.PhongView.PhongViewShowClient;
using APISoluton.Database.ViewModel.RoleView;
using APISoluton.Database.ViewModel.UserView;
using APISoluton.Database.ViewModel.UserView.UserViewShow;
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
            CreateMap<PhieuDatPhong, AddPhieuDatPhongView>();
            CreateMap<AddPhieuDatPhongView, PhieuDatPhong>();
            CreateMap<DichVuVM, DichVu>();
            CreateMap<DichVu, DichVuVM>();
        }
    }
}

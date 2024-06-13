using APISolution.Database.Entity;
using APISoluton.Application.ViewModel.LoaiPhongView;
using APISoluton.Application.ViewModel.PhongView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Interface.LoaiPhong.Commands
{
    public interface ILoaiPhongCommand
    {
        Task<LoaiPhongVM> AddLoaiPhong(LoaiPhongVM model);
        Task<string> RemovePhong(int id);
        Task<string> UpdatePhong(LoaiPhongVM model, int id);
    }
}

using APISolution.Database.Entity;
using APISoluton.Database.ViewModel.LoaiPhongView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Interface.ILoaiPhong.Commands
{
    public interface ILoaiPhongCommand
    {
        Task<LoaiPhongVM> AddLoaiPhong(LoaiPhongVM model);
        Task RemovePhong(int id);
        Task UpdateLoaiPhong(LoaiPhongVM model, int id);
    }
}

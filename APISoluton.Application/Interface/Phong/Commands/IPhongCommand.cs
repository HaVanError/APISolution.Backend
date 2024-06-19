using APISoluton.Database.ViewModel.PhongView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Interface.Phong.Commands
{
    public interface IPhongCommand
    {
        Task<PhongVM> AddPhong(PhongVM model);
        Task RemovePhong(int id);
        Task UpdatePhong(PhongVM model , int id);
    }
}

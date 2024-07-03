using APISolution.Database.ViewModel.PhongView;
using APISoluton.Database.ViewModel.PhongView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Interface.IPhong.Commands
{
    public interface IPhongCommand
    {
        Task<AddPhongView> AddPhong(AddPhongView model);
        Task RemovePhong(int id);
        Task UpdatePhong(PhongVM model , int id);
    }
}

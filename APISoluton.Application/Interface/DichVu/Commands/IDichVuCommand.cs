using APISoluton.Database.ViewModel.DichVuView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Interface.DichVu.Commands
{
    public interface IDichVuCommand
    {
        Task<DichVuVM> AddDichVu(DichVuVM model);
        Task<DichVuVM> UpdateDichVu(DichVuVM model,int id);
        Task DeleteDichVu(int id);
    }
}

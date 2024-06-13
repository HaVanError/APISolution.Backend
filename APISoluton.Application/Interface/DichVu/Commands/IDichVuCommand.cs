using APISoluton.Application.ViewModel.DichVuView;
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
        Task<IEnumerable<DichVuVM>> UpdateDichVu(DichVuVM model,int id);
        Task DeleteDichVu(int id);
    }
}

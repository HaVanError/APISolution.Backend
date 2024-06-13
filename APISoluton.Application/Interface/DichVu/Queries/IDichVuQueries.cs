using APISoluton.Application.ViewModel.DichVuView;
using APISoluton.Application.ViewModel.UserView.UserViewShow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Interface.DichVu.Queries
{
    public interface IDichVuQueries
    {
        Task<List<DichVuVM>>GetAllDichVu();
      
        Task<IEnumerable<DichVuVM>>GetByIdDichVu(int id);
    }
}

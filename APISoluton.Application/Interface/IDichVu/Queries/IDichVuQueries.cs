using APISoluton.Database.ViewModel.DichVuView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Interface.IDichVu.Queries
{
    public interface IDichVuQueries
    {
        Task<List<DichVuVM>>GetAllDichVu(int pageNumber, int pageSize);
      
        Task<DichVuVM>GetByIdDichVu(int id);
    }
}

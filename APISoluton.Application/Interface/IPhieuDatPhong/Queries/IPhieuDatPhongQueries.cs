using APISolution.Database.Entity;
using APISolution.Database.ViewModel;
using APISoluton.Database.ViewModel.PhieuDatPhongView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Interface.IPhieuDatPhong.Queries
{
    public interface IPhieuDatPhongQueries
    {
        Task<List<PhieuDatPhongVM>> GetAllPhieuDatPhong(int pageNumber ,int pageSize);
        Task<List<PhieuDatPhong>> GetByAllPhieuDatPhong(ViewSeach model);

    }
}

using APISoluton.Database.ViewModel.PhieuDatPhongView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Interface.PhieuDatPhong.Queries
{
    public interface IPhieuDatPhongQueries
    {
        Task<List<PhieuDatPhongVM>> GetAllPhieuDatPhong();
        Task<PhieuDatPhongVM> GetByIdPhieuDatPhong(int id);

    }
}

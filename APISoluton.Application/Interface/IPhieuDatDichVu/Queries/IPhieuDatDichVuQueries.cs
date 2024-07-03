using APISolution.Database.Entity;
using APISolution.Database.ViewModel.PhieuDatDichVuView;
using APISolution.Database.ViewModel.PhieuDatDichVuView.PhieuDatDichVuViewShow;
using APISoluton.Database.ViewModel.PhieuDatPhongView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Interface.IPhieuDatDichVu.Queries
{
    public interface IPhieuDatDichVuQueries
    {
        Task<List<PhieuDatDichVuVM>>GetAllPhieuDichVu(int pageNumber, int pageSize);
        Task<List<PhieuDichVu>> GetByAllPhieuDichVu(SearchViewPhieuDatDV model);
    }
}

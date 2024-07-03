using APISolution.Database.ViewModel.PhieuDatDichVuView;
using APISolution.Database.ViewModel.PhieuDatDichVuView.PhieuDatDichVuViewShow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Interface.IPhieuDatDichVu.Commands
{
    public interface IPhieuDatDichVuCommand
    {
        Task<AddPhieuDatDichVuView> AddPhieuDatDichVu(AddPhieuDatDichVuView model);
        Task UpdatePhieuDatDichVu(int id, AddPhieuDatDichVuView model);
        Task DeletePhieuDatDichVu(int id);
    }
}

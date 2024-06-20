using APISolution.Database.Entity;
using APISolution.Database.ViewModel.PhieuDatPhongView.PhieuDatPhongViewShow;
using APISoluton.Database.ViewModel.PhieuDatPhongView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Interface.PhieuDatPhong.Commands
{
    public interface IPhieuDatPhongCommand
    {
        Task<PhieuDatPhongVM>DatPhong(AddPhieuDatPhongView mode);
    }
}

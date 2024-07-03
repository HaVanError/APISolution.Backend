using APISolution.Database.ViewModel.ThanhToanView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Interface.IThanhToan.Commands
{
    public interface IThanhToanCommand
    {
        Task<List<ThanhToanVM>> ThanhToan(ThanhToanAddVM model);
        Task DuyetThanhToan(int idThanhToan);
    }
}

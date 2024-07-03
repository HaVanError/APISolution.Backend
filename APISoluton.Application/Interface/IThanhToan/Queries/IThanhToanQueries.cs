using APISolution.Database.ViewModel.ThanhToanView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Interface.IThanhToan.Queries
{
    public interface IThanhToanQueries 
    {
        Task<List<ThanhToanVM>> GetListThanhToan(int pageNumber, int pageSize);
    }
}

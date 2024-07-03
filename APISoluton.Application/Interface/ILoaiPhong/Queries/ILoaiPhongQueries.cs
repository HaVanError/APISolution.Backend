using APISoluton.Database.ViewModel.LoaiPhongView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Interface.ILoaiPhong.Queries
{
    public interface ILoaiPhongQueries
    {
        Task<List<LoaiPhongVM>> GetAllLoaiPhong();
        Task<LoaiPhongVM> GetLoaiPhongById(int id);
    }
}

using APISoluton.Database.ViewModel.PhongView;
using APISoluton.Database.ViewModel.PhongView.PhongViewShowClient;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.Interface.Phong.Queries
{
    public interface IPhongQueries
    {
        Task<List<PhongVMShow>> GetAllPhong(int pageNumber, int pageSize);
        Task<PhongVM> GetPhongById(int id);
    }
}

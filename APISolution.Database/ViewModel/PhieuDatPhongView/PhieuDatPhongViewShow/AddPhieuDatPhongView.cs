using APISolution.Database.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISolution.Database.ViewModel.PhieuDatPhongView.PhieuDatPhongViewShow
{
    public class AddPhieuDatPhongView
    {
        public string TenNguoiDat { get; set; } = string.Empty;
        public string SoDienThoai { get; set; } = string.Empty;
        public int IdPhong { get; set; }

    }
}

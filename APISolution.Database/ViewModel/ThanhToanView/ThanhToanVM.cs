using APISolution.Database.Entity;
using APISolution.Database.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISolution.Database.ViewModel.ThanhToanView
{
    public class ThanhToanVM
    {
     
        public string TenPhong { get; set; } = string.Empty;
        public string TenKhachHang { get; set; } = string.Empty;
        public double TongThanhTien { get; set; }
        public int IdPhieuDatPhong {  get; set; }
        public string TrangThaiThanhToan { get; set; }
    }
}

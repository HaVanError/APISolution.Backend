using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISolution.Database.ViewModel.PhieuDatDichVuView
{
    public class PhieuDatDichVuVM
    {
        public string TenPhong { get; set; } = string.Empty;
        public string TenKhachHang { get; set; } = string.Empty;
        public int SoLuong { get; set; } //
        public string TenDichVu { get; set; } = string.Empty;//
        public int IdPhong { get; set; } //
        public int IdDichVu { get; set; }   //
        public int IdPhieuDatPhong { get; set; } //
        public float Gia { get; set; }
        public double ThanhTien { get; set; }
        public string NgayDatDichVu { get; set; }
    }
}

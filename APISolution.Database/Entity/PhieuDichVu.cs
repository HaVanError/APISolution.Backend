using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISolution.Database.Entity
{
    public class PhieuDichVu
    {
        public int IdPhieuDichVu {  get; set; }
        public string TenPhong {  get; set; } = string.Empty;
        public string TenKhachHang { get; set; } = string.Empty;
        public int SoLuong {  get; set; } 
        public string TenDichVu { get; set; } = string.Empty;
        public int IdPhong {  get; set; }
        public Phong ?Phong { get; set; } 
        public int IdDichVu { get; set; }   
        public DichVu ?DichVu { get; set; }
        public int IdPhieuDatPhong {  get; set; }
        public PhieuDatPhong ?PhieuDatPhong { get; set; }
        public float Gia {  get; set; }
        public double ThanhTien {  get; set; }

    }
}

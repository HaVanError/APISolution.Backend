using APISolution.Database.Enum;
using APISolution.Database.ViewModel.PhieuDatDichVuView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISolution.Database.Entity
{
    public class ThanhToan
    {
        public int IdThanhToan { get; set; }
        public string TenPhong {  get; set; } = string.Empty;
        public string TenKhachHang { get; set; } = string.Empty;
        public double TongThanhTien {  get; set; }
        public TrangThaiThanhToan TrangThaiThanhToan { get; set; }
        public int? idPhieuDatPhong { get; set; }
     
       // public List<ThanhToan_PhieuDatPhong> PhieuThanhToan_DatPhong { get; set; }
      //  public ICollection<PhieuDichVu> PhieuDatDichVus { get; set; }
        public DateTime NgayThanhToan { get; set; }
        public DateTime NgayTraPhong { get; set; }

        public PhieuDatPhong PhieuDatPhong { get; set; }

    }
}

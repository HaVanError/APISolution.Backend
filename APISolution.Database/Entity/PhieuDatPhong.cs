using APISolution.Database.Enum;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISolution.Database.Entity
{
    public class PhieuDatPhong
    {
        public int IdPhieuDatPhong {  get; set; }
        public string TenPhong {  get; set; } = string.Empty;
        public float GiaPhong {  get; set; } 
        public string TenNguoiDat {  get; set; } = string.Empty;
        public string SoDienThoai {  get; set; } = string.Empty;
        public TrangThaiPhieuDatPhong Status { get; set; }
        public List<PhieuDichVu>?PhieuDichVus { get; set; }
        public int IdPhong { get; set; }
        public Phong ?Phong {  get; set; }
        public DateTime NgayDatPhong { get; set; }

        public ICollection<ThanhToan> ThanhToans { get; set; }
        //public List<ThanhToan_PhieuDatPhong> PhieuThanhToan_DatPhong { get; set; }

    }
}

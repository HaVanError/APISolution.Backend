using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISolution.Database.ViewModel.PhieuDatDichVuView.PhieuDatDichVuViewShow
{
    public class AddPhieuDatDichVuView
    {
        public string TenDichVu { get; set; } = string.Empty;//
        public int SoLuong { get; set; } //
        public int IdDichVu { get; set; }   //
        public int IdPhieuDatPhong { get; set; } //
        public DateTime NgayDatDichVu { get; set; }
    }
}

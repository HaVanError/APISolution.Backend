using APISolution.Database.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISolution.Database.Entity
{
    public class Phong
    {
        public int IdPhong {  get; set; }
        public string Name { get; set; } = string.Empty;
        public string Describe { get; set; } = string.Empty;
        public int IdLoaiPhong {  get; set; }
        public LoaiPhong LoaiPhongs { get; set; } =default!;
        public StatusPhong StatusPhong { get; set; }
        public PhieuDatPhong PhieuDatPhongs {  get; set; } =default!;
        public List<PhieuDichVu>? PhieuDichVus { get; set; }  

        public float GiaPhong { get; set; }
        
    }
}

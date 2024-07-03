using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISolution.Database.Entity
{
    public class DichVu
    {
        public int IdDichVu {  get; set; }
        public string NameDichVu { get; set; } = string.Empty;
        public int SoLuong {  get; set; }  
        public float Gia { get; set; }
        public List<PhieuDichVu> ?PhieuDichVus { get; set; }
       


    }
}

using APISolution.Database.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Database.ViewModel.DichVuView
{
    public class DichVuVM
    {
        public string NameDichVu { get; set; } = string.Empty;
        public int SoLuong { get; set; }
        public float Gia { get; set; }

    }
}

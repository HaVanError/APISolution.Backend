using APISolution.Database.Entity;
using APISolution.Database.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.ViewModel.PhongView.PhongViewShowClient
{
    public class PhongVMShow
    {
        public string Name { get; set; } = string.Empty;
        public string Describe { get; set; } = string.Empty;
        public string StatusPhong { get; set; }
        public string LoaiPhong { get;  set; }
        public float GiaPhong { get; set; }
    }
}

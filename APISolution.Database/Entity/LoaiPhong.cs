using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISolution.Database.Entity
{
    public class LoaiPhong
    {
        public int IdLoaiPhong { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Phong> ?Phong { get; set; }
        public string MoTa { get; set; } = string.Empty;

    }
}

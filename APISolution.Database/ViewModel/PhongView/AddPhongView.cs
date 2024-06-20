using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISolution.Database.ViewModel.PhongView
{
    public class AddPhongView
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int IdLoaiPhong { get; set; }
        public float Gia {  get; set; }

    }
}

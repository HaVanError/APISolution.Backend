using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISolution.Database.Entity
{
    public class ThanhToan_PhieuDatPhong
    {
        public int Id { get; set; }
        public int IdPhieuDatPhong { get; set; }
        public PhieuDatPhong PhieuDatPhong { get; set; }
        public int IdThanhToan { get; set; }
        public ThanhToan ThanhToan { get; set; }
        public DateTime NgayDat { get; set; }
        public DateTime NgayTra { get; set; }

    }
}

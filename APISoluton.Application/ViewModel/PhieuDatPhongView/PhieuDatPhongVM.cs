﻿using APISolution.Database.Entity;
using APISolution.Database.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.ViewModel.PhieuDatPhongView
{
    public class PhieuDatPhongVM
    {
        public string TenPhong { get; set; } = string.Empty;
        public string GiaPhong { get; set; } = string.Empty;
        public string TenNguoiDat { get; set; } = string.Empty;
        public string SoDienThoai { get; set; } = string.Empty;
        public TrangThaiPhieuDatPhong Status { get; set; }
      
        public int IdPhong { get; set; }
      
    }
}

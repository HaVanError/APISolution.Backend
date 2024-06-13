﻿using APISolution.Database.Entity;
using APISolution.Database.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoluton.Application.ViewModel.PhongView
{
    public class PhongVM
    {
        public string Name { get; set; } = string.Empty;
        public string Describe { get; set; } = string.Empty;
        public int IdLoaiPhong { get; set; }
        public StatusPhong StatusPhong { get; set; }
        public float GiaPhong { get; set; }
    }
}

﻿using APISolution.Database.Configuration;
using APISolution.Database.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISolution.Database.DatabaseContext
{
    public class DdConnect : DbContext
    {
        public DdConnect(DbContextOptions<DdConnect> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ResfreshToken> ResfreshTokens { get; set; }
        public DbSet<Phong> Phongs { get; set; }
        public DbSet<DichVu> DichVus { get; set; }
        public DbSet<PhieuDatPhong> PhieuDatPhongs { get; set; }
        public DbSet<PhieuDichVu> PhieuDichVus { get; set; }
        public DbSet<LoaiPhong> Loais { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ConfigurationUser());
            modelBuilder.ApplyConfiguration(new ConfigurationRole());
            modelBuilder.ApplyConfiguration(new ConfigurationResfreshToken());
            modelBuilder.ApplyConfiguration(new ConfigurationPhong());
            modelBuilder.ApplyConfiguration(new ConfigurationLoaiPhong());
            modelBuilder.ApplyConfiguration(new ConfigurationDichVu());
            modelBuilder.ApplyConfiguration(new ConfigurationPhieuDatPhong());
            modelBuilder.ApplyConfiguration(new ConfigurationPhieuDichVu());

        }
    }
}

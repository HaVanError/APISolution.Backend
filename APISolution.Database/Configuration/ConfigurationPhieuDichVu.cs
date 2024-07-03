using APISolution.Database.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISolution.Database.Configuration
{
    public class ConfigurationPhieuDichVu : IEntityTypeConfiguration<PhieuDichVu>
    {
        public void Configure(EntityTypeBuilder<PhieuDichVu> builder)
        {
          
            builder.ToTable("PhieuDatDichVu");
            builder.HasKey(x=>x.IdPhieuDichVu);
            builder.HasIndex(x => new { x.TenDichVu, x.IdPhieuDichVu });
            builder.HasOne(x => x.DichVu).WithMany(x => x.PhieuDichVus).HasForeignKey(x => x.IdDichVu)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.PhieuDatPhong).WithMany(x => x.PhieuDichVus).HasForeignKey(x => x.IdPhieuDatPhong)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

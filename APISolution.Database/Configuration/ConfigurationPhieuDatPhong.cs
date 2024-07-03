using APISolution.Database.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISolution.Database.Configuration
{
    public class ConfigurationPhieuDatPhong : IEntityTypeConfiguration<PhieuDatPhong>
    {
        public void Configure(EntityTypeBuilder<PhieuDatPhong> builder)
        {
            builder.ToTable("PhieuDatPhong");
            builder.HasKey(x=>x.IdPhieuDatPhong);
            builder.HasIndex(x=> new {x.TenPhong, x.TenNguoiDat });
            builder.HasOne(x => x.Phong).WithOne(x => x.PhieuDatPhongs).HasForeignKey<PhieuDatPhong>(X=>X.IdPhong).OnDelete(DeleteBehavior.Restrict);
           

        }
    }
}

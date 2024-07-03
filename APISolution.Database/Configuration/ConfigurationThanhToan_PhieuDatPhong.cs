using APISolution.Database.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISolution.Database.Configuration
{
    public class ConfigurationThanhToan_PhieuDatPhong : IEntityTypeConfiguration<ThanhToan_PhieuDatPhong>
    {
        public void Configure(EntityTypeBuilder<ThanhToan_PhieuDatPhong> builder)
        {
            builder.ToTable("ThanhToan_PhieuDatPhong");
            builder.HasKey(x => x.Id);
            //builder.HasOne(x => x.PhieuDatPhong).WithMany(x => x.PhieuThanhToan_DatPhong).HasForeignKey(x => x.IdPhieuDatPhong).OnDelete(DeleteBehavior.Cascade);
            //builder.HasOne(x => x.ThanhToan).WithMany(x => x.PhieuThanhToan_DatPhong).HasForeignKey(x => x.IdThanhToan).OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}

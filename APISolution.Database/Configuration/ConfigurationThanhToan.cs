using APISolution.Database.Entity;
using APISolution.Database.Enum;
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
    public class ConfigurationThanhToan : IEntityTypeConfiguration<ThanhToan>
    {
        public void Configure(EntityTypeBuilder<ThanhToan> builder)
        {
            builder.HasKey(x => x.IdThanhToan);
            builder.ToTable("ThanhToan");
            builder.HasOne(x => x.PhieuDatPhong).WithMany(x => x.ThanhToans).HasForeignKey(x => x.idPhieuDatPhong).OnDelete(DeleteBehavior.SetNull);
            builder.Property(x => x.TrangThaiThanhToan).HasDefaultValue(TrangThaiThanhToan.Pending).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);
            builder.Property(x => x.idPhieuDatPhong).IsRequired(false);
        }
    }
}

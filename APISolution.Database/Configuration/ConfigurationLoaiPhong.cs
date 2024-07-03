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
    public class ConfigurationLoaiPhong : IEntityTypeConfiguration<LoaiPhong>
    {
        public void Configure(EntityTypeBuilder<LoaiPhong> builder)
        {
            builder.ToTable("LoaiPhong");
            builder.HasKey(x => x.IdLoaiPhong);
            builder.HasIndex(x => new { x.Name, x.IdLoaiPhong }).IsUnique();


            builder.HasData(
                 new LoaiPhong { IdLoaiPhong =1,MoTa="Loại phòng VIP",Name="Vip"},
                 new LoaiPhong { IdLoaiPhong = 2, MoTa = "Loại phòng Thường", Name = "Thường" }
                );
        }
    }
}

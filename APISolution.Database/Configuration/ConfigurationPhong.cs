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
    public class ConfigurationPhong : IEntityTypeConfiguration<Phong>
    {
        public void Configure(EntityTypeBuilder<Phong> builder)
        {
            builder.ToTable("Phong");
            builder.HasKey(x=>x.IdPhong);
            builder.HasIndex(x=> new {x.IdPhong,x.Name}).IsUnique(false);
            builder.HasOne(x => x.LoaiPhongs).WithMany(x=>x.Phong).HasForeignKey(x=>x.IdLoaiPhong).OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.StatusPhong).HasDefaultValue(StatusPhong.Empty).IsRequired();

            builder.HasData(
                new Phong { IdPhong = 1,Describe="Phòng Vip A1",IdLoaiPhong =1,Name="A1",GiaPhong =200000,StatusPhong=StatusPhong.Empty },
                  new Phong { IdPhong = 2, Describe = "Phòng Vip A2", IdLoaiPhong = 1, Name = "A2", GiaPhong = 200000, StatusPhong = StatusPhong.Empty },
                    new Phong { IdPhong = 3, Describe = "Phòng Vip A3", IdLoaiPhong = 1, Name = "A3", GiaPhong = 200000, StatusPhong = StatusPhong.Empty },
                      new Phong { IdPhong = 4, Describe = "Phòng Vip B1", IdLoaiPhong = 2, Name = "B1", GiaPhong = 150000, StatusPhong = StatusPhong.Empty },
                      new Phong { IdPhong = 5, Describe = "Phòng Vip B2", IdLoaiPhong = 2, Name = "B2", GiaPhong = 150000, StatusPhong = StatusPhong.Empty }
                );

        }
    }
}

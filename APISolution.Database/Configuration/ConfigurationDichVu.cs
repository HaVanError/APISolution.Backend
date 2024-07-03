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
    internal class ConfigurationDichVu : IEntityTypeConfiguration<DichVu>
    {
        public void Configure(EntityTypeBuilder<DichVu> builder)
        {
            builder.ToTable("DichVu");
            builder.HasKey(x => x.IdDichVu);
            builder.HasIndex(x => new {x.IdDichVu,x.NameDichVu});

            builder.HasData(
                new DichVu { IdDichVu =1 ,NameDichVu ="Sting",SoLuong =1000,Gia = 15000},
                  new DichVu { IdDichVu = 2, NameDichVu = "Bò hút", SoLuong = 1000, Gia = 15000 },
                    new DichVu { IdDichVu = 3, NameDichVu = "Bia Sài Gòn", SoLuong = 1000, Gia = 18000 },
                      new DichVu { IdDichVu = 4, NameDichVu = "Thuốc lá Hero", SoLuong = 1000, Gia = 22000 }
                        
                );
        }
    }
}

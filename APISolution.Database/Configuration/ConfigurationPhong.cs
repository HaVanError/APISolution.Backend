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

        }
    }
}

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
            
        }
    }
}

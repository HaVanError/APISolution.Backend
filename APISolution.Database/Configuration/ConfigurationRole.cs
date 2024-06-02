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
    public class ConfigurationRole : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Quyen");
            builder.HasKey(x => x.idRole);
            builder.HasIndex(x=> new {x.idRole,x.NameRole});
            builder.Property(x => x.NameRole).IsRequired();
            builder.Property(x => x.MoTa).IsRequired();
            
        }
    }
}


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
    public class ConfigurationUser : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("UserInformation");
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Name).HasDefaultValue(50).IsRequired();
            builder.Property(x => x.Address).HasDefaultValue(200).IsRequired();
            builder.Property(x => x.City).HasDefaultValue(100).IsRequired();
          builder.HasOne(x => x.Role).WithOne(x => x.User).HasForeignKey<User>(x => x.IdRole).OnDelete(DeleteBehavior.Restrict);
            

        }
    }
}

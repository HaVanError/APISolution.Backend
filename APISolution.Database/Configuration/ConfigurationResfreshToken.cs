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
    public class ConfigurationResfreshToken : IEntityTypeConfiguration<ResfreshToken>
    {
        public void Configure(EntityTypeBuilder<ResfreshToken> builder)
        {
            builder.HasKey(x => x.IdToken);
            builder.HasOne(x => x.User).WithOne(x => x.ResfreshToken).HasForeignKey<ResfreshToken>(x => x.idUser).OnDelete(DeleteBehavior.Cascade);
        }
    }
}

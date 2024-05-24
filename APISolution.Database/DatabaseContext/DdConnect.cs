using APISolution.Database.Configuration;
using APISolution.Database.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISolution.Database.DatabaseContext
{
    public class DdConnect : DbContext
    {
        public DdConnect(DbContextOptions<DdConnect> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ConfigurationUser());
            modelBuilder.ApplyConfiguration(new ConfigurationRole());

        }
    }
}

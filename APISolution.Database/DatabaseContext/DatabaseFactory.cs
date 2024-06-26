﻿
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.EntityFrameworkCore;
namespace APISolution.Database.DatabaseContext
{
    public class DatabaseFactory : IDesignTimeDbContextFactory<DdConnect>
    {
        public DdConnect CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
            var connectionstring = configuration.GetConnectionString("db");
            var optionBuider = new DbContextOptionsBuilder<DdConnect>();
            optionBuider.UseSqlServer(connectionstring);
            return new DdConnect(optionBuider.Options);
        }

        
    }
}

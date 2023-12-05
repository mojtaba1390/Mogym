using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Mogym.Domain.Entities.Log;

namespace Mogym.Domain.Context
{
    public class MogymLogContext:DbContext
    {
        public MogymLogContext(DbContextOptions<MogymLogContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("MogymLogConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }


        public DbSet<SeriLog> SeriLog { get; set; }
        public DbSet<UserLogging> UserLogging { get; set; }
        public DbSet<SmsLog> SmsLog { get; set; }
    }
}

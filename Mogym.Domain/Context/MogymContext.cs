using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Mogym.Domain.Configuration;
using Mogym.Domain.Entities;

namespace Mogym.Domain.Context
{
    public class MogymContext:DbContext
    {
        public MogymContext(DbContextOptions<MogymContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("MogymConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        }


        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Permission> Accessibility { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<RolePermission> RolePermission { get; set; }
        public DbSet<Menu> Menu { get; set; }

    }
}

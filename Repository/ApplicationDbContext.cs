using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Domain.Entity;
using Repository.EntityTypeConfiguration;

namespace Repository
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<CarEntity> Cars { get; set; }


        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        { }

      
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.ApplyConfiguration(new ClientEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CarEntityTypeConfiguration());
            
        }

        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.EntityTypeConfiguration
{
   

    public class CarEntityTypeConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> entityTypeBuilder)
        {

            entityTypeBuilder.Navigation(car => car.Client)
                .AutoInclude();


            entityTypeBuilder
                .HasIndex(car => car.VIN)
                .IsUnique();
        }
    }
}

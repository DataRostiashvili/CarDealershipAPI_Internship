using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entity;

namespace Repository.EntityTypeConfiguration
{
    public class ClientEntityTypeConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> entityTypeBuilder)
        {

            entityTypeBuilder.Navigation(client => client.Cars)
                .AutoInclude();

            entityTypeBuilder.Navigation(client => client.ClientContactInfo)
                .AutoInclude();


            entityTypeBuilder
                .HasIndex(client => client.IDNumber)
                .IsUnique();
        }
    }
}

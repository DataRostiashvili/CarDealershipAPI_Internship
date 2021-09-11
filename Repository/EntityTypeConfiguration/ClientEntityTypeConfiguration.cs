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
    public class ClientEntityTypeConfiguration : IEntityTypeConfiguration<ClientEntity>
    {
        public void Configure(EntityTypeBuilder<ClientEntity> entityTypeBuilder)
        {

            entityTypeBuilder.Navigation(client => client.Cars)
                .AutoInclude();

            entityTypeBuilder.Navigation(client => client.ClientContactInfoEntity)
                .AutoInclude();

            entityTypeBuilder
                .HasOne(clientEntity => clientEntity.ClientContactInfoEntity)
                .WithOne(clientContactInfoEntity => clientContactInfoEntity.ClientEntity)
                .HasForeignKey<ClientContactInfoEntity>(clientContactInfoEntity => clientContactInfoEntity.ClientEntityId);


            entityTypeBuilder
                .HasIndex(client => client.IDNumber)
                .IsUnique();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGC.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGC.Infrastructure.EntityConfig
{
    public class ContatoMap : IEntityTypeConfiguration<Contato>
    {
        public void Configure(EntityTypeBuilder<Contato> builder)
        {
            builder
                .HasKey(con => con.ContatoId);

            builder
                .HasOne(con => con.Cliente)//<==Está dizendo que contato só tem um cliente
                .WithMany(con => con.Contatos)//<==Está dizendo que um cliente pode ter muitos contatos
                .HasForeignKey(con => con.ClienteId)    
                .HasPrincipalKey(con => con.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(e => e.Nome)
               .HasColumnType("varchar(200)")
               .IsRequired();

            builder
                .Property(e => e.Email)
               .HasColumnType("varchar(100)")
               .IsRequired();

            builder
                .Property(e => e.Telefone)
               .HasColumnType("varchar(15)");
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGC.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGC.Infrastructure.EntityConfig
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder
                .HasKey(c => c.ClienteId);//<== Está dizendo que ClienteId é uma chave primária

            builder
                .HasMany(c => c.Contatos)//<== Está dizendo que ClienteId é uma chave estrangeira na tabela Contato
                .WithOne(c => c.Cliente)
                .HasForeignKey(c => c.ClienteId)//<== Está dizendo que ClienteId é uma chave estrangeira na tabela Contato
                .HasPrincipalKey(c => c.ClienteId)//<== Está dizendo que ClienteId é uma chave estrangeira principal na tabela Contato
                .OnDelete(DeleteBehavior.Restrict);//Comando para não deletar em cascata
                                                   //.OnDelete(DeleteBehavior.Cascade);//Comando para deletar em cascata
                                                   //.OnDelete(DeleteBehavior.SetNull);//Comando para deletar em cascata porém as chaves estrageiras das tabelas filhas ficaram com status Null

            builder//Relacionamento de UM para UM
                .HasOne(x => x.Endereco)
                .WithOne(x => x.Cliente)
                .HasForeignKey<Endereco>(x => x.ClienteId);

            builder
                .Property(e => e.CPF)
                .HasColumnType("varchar(11)")
                .IsRequired();

            builder
                .Property(e => e.Nome)
                .HasColumnType("varchar(200)")
                .IsRequired();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using SGC.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGC.Infrastructure.Data
{
    public class ClienteContext : DbContext
    {
        public ClienteContext(DbContextOptions<ClienteContext> options) : base(options)
        {

        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Contato> Contatos { get; set; }
        public DbSet<Profissao> Profissoes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Cliente>().ToTable("Cliente");
            modelBuilder.Entity<Contato>().ToTable("Contato");
            modelBuilder.Entity<Profissao>().ToTable("Profissao");
            modelBuilder.Entity<Endereco>().ToTable("Endereco");
            modelBuilder.Entity<ProfissaoCliente>().ToTable("ProfissaoCliente");

            #region Configurações de Cliente
            modelBuilder.Entity<Cliente>()
                .HasKey(c => c.ClienteId);//<== Está dizendo que ClienteId é uma chave primária

            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Contatos)//<== Está dizendo que ClienteId é uma chave estrangeira na tabela Contato
                .WithOne(c => c.Cliente)
                .HasForeignKey(c => c.ClienteId)//<== Está dizendo que ClienteId é uma chave estrangeira na tabela Contato
                .HasPrincipalKey(c => c.ClienteId);//<== Está dizendo que ClienteId é uma chave estrangeira principal na tabela Contato

            modelBuilder.Entity<Cliente>().Property(e => e.CPF)
                .HasColumnType("varchar(11)")
                .IsRequired();

            modelBuilder.Entity<Cliente>().Property(e => e.Nome)
                .HasColumnType("varchar(200)")
                .IsRequired();
            #endregion

            #region Configurações de Contato
            modelBuilder.Entity<Contato>()
                .HasKey(con => con.ContatoId);

            modelBuilder.Entity<Contato>()
                .HasOne(con => con.Cliente)//<==Está dizendo que contato só tem um cliente
                .WithMany(con => con.Contatos)//<==Está dizendo que um cliente pode ter muitos contatos
                .HasForeignKey(con => con.ClienteId)
                .HasPrincipalKey(con => con.ClienteId);

            modelBuilder.Entity<Contato>().Property(e => e.Nome)
               .HasColumnType("varchar(200)")
               .IsRequired();
            modelBuilder.Entity<Contato>().Property(e => e.Email)
               .HasColumnType("varchar(100)")
               .IsRequired();
            modelBuilder.Entity<Contato>().Property(e => e.Telefone)
               .HasColumnType("varchar(15)");
            #endregion

            #region Configurações de Profissao
            modelBuilder.Entity<Profissao>().Property(p => p.Nome)
               .HasColumnType("varchar(400)")
               .IsRequired();

            modelBuilder.Entity<Profissao>().Property(p => p.CBO)
              .HasColumnType("varchar(10)")
              .IsRequired();

            modelBuilder.Entity<Profissao>().Property(p => p.Descricao)
              .HasColumnType("varchar(1000)")
              .IsRequired();
            #endregion

            #region Configurações de Endereco
            modelBuilder.Entity<Endereco>().Property(end => end.Bairro)
             .HasColumnType("varchar(200)")
             .IsRequired();

            modelBuilder.Entity<Endereco>().Property(end => end.CEP)
            .HasColumnType("varchar(15)")
            .IsRequired();

            modelBuilder.Entity<Endereco>().Property(end => end.Logradouro)
            .HasColumnType("varchar(200)")
            .IsRequired();

            modelBuilder.Entity<Endereco>().Property(end => end.Numero)
           .HasColumnType("varchar(10)")
           .IsRequired();

            modelBuilder.Entity<Endereco>().Property(end => end.Referencia)
           .HasColumnType("varchar(400)");
            #endregion

            #region Configurações de ProfissaoCliente
            modelBuilder.Entity<ProfissaoCliente>().HasKey(pc => pc.Id);

            modelBuilder.Entity<ProfissaoCliente>()
                .HasOne(pc => pc.Cliente)
                .WithMany(pc => pc.ProfissaoCLientes)
                .HasForeignKey(pc => pc.ClienteId);

            modelBuilder.Entity<ProfissaoCliente>()
                .HasOne(pc => pc.Profissao)
                .WithMany(pc => pc.ProfissaoClientes)
                .HasForeignKey(pc => pc.ProfissaoId);

            #endregion

            #region Configurações de Menu
            modelBuilder.Entity<Menu>()
                .HasMany(x => x.SubMenu)
                .WithOne()
                .HasForeignKey(x => x.MenuId);
            #endregion
        }


    }
}

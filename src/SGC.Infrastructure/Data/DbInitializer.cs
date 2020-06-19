using SGC.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGC.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ClienteContext context)
        {
            //Verifica se o BD Cliente já foi populado
            if (context.Clientes.Any())
            {
                return;
            }

            var clientes = new Cliente[]
            {
                new Cliente
                {
                    Nome = "Fulano da Silva",
                    CPF = "12345678901"
                },
                new Cliente
                {
                    Nome = "Beltrano da Silva",
                    CPF = "98765432101"
                }
            };

            context.AddRange(clientes);

            var contatos = new Contato[]
            {
                new Contato
                {
                    Nome = "Contato 1",
                    Telefone = "999999999",
                    Email = "emailcontato1@gmail.com",
                    Cliente = clientes[0]
                },
                new Contato
                {
                    Nome = "Contato 2",
                    Telefone = "888888888",
                    Email = "emailcontato2@gmail.com",
                    Cliente = clientes[1]
                }
            };

            context.AddRange(contatos);
            context.SaveChanges();
        }
    }
}

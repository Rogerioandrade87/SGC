using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SGC.ApplicationCore.Interfaces.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        // TEntity é um tipo generico
        TEntity Adicionar(TEntity entity);
        void Atualizar(TEntity entity);
        IEnumerable<TEntity> ObterTodos();
        TEntity ObterPorId(int id);
        //TEntity ObterPorId(string nome);
        IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicado);//<== Expression Lambda
        void Remover(TEntity entity);

    }
}

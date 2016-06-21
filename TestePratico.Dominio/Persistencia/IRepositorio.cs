using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace TestePratico.Dominio.Persistencia
{
    public interface IRepositorio<T>
    {
        void Inserir(T obj);
        void Alterar(T obj);
        void Gravar(T obj);
        void Excluir(T obj);

        T ObterPorId(int id);

        IQueryable<T> Consultar();

        IQueryable<T> Consultar(Expression<Func<T, bool>> expressao);

        int Count();

        int Count(Expression<Func<T, bool>> expressao);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Linq;

namespace TestePratico.Dominio.Persistencia
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {

        public Repositorio(IUnidadeTrabalho unidadeTrabalho)
        {
            UnidadeTrabalho = unidadeTrabalho;
        }

        /// <summary>
        /// Obter ou definir a unidade de trabalho
        /// </summary>
        public IUnidadeTrabalho UnidadeTrabalho { get; set; }


        public ISession Sessao
        {
            get
            {
                return ((Unidade_Trabalho)UnidadeTrabalho).Sessao;
            }
        }


        public void Inserir(T obj)
        {
            Sessao.Save(obj);
        }

        public void Alterar(T obj)
        {
            Sessao.Update(obj);
        }

        public void Gravar(T obj)
        {
            Sessao.SaveOrUpdate(obj);
        }

        public void Excluir(T obj)
        {
            Sessao.Delete(obj);            
        }

        public T ObterPorId(int id)
        {
            return Sessao.Get<T>(id);
        }

        public IQueryable<T> Consultar()
        {
            IQueryable<T> iq = Sessao.Query<T>();
            return iq;            
        }

        public IQueryable<T> Consultar(System.Linq.Expressions.Expression<Func<T, bool>> expressao)
        {
            IQueryable<T> iq = Sessao.Query<T>().Where(expressao);
            return iq;            
        }

        public int Count()
        {
            IQueryable<T> iq = Sessao.Query<T>();
            int count = iq.Count();
            return count;
        }

        public int Count(System.Linq.Expressions.Expression<Func<T, bool>> expressao)
        {
            IQueryable<T> iq = Sessao.Query<T>().Where(expressao);
            int count = iq.Count();
            return count;
        }
    }
}

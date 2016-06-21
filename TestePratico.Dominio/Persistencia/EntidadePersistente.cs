using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestePratico.Dominio.Infra;
using System.Linq.Expressions;

namespace TestePratico.Dominio.Persistencia
{
    public abstract class EntidadePersistente<T, K>
        where T : EntidadePersistente<T, K>
        where K : IRepositorio<T>
    {

        /// <summary>
        /// Entidade do repositório
        /// </summary>
        private K repositorioLazy;

        /// <summary>
        /// Este atributo vai guardar uma referência a própria entidade abstrata;
        /// </summary>
        private T entidade;

        public EntidadePersistente()
            : base()
        {
            this.Id = 0;
            this.entidade = (dynamic)this;
        }
        /// <summary>
        /// Objeto identificador de todas as classes
        /// </summary>
        public virtual int Id { get; protected set; }

        /// <summary>
        /// Repositório da entidade
        /// </summary>
        private K Repositorio
        {
            get
            {
                if (repositorioLazy == null)
                {
                    repositorioLazy = Fabrica.Instancia.Obter<K>();
                }

                return repositorioLazy;
            }
        }

        /// <summary>
        /// Obtém uma instancia fazendo uma busca no banco de dados pelo identificador informado.
        /// </summary>
        /// <param name="id">ID do objeto.</param>
        /// <returns>Objeto tipo T.</returns>
        public static T ObterPorId(int id)
        {
            var t = ObterRepositorio().ObterPorId(id);
            return t;
        }

        /// <summary>
        /// Obtém um objeto de consulta que implementa a interface <see cref="IQueryable"/>
        /// </summary>
        /// <returns>Objeto <see cref="IQueryable"/>.</returns>
        public static IQueryable<T> Consultar()
        {
            var iq = ObterRepositorio().Consultar();
            return iq;
        }

        /// <summary>
        /// Obtém um objeto de consulta que implementa a interface <see cref="IQueryable"/>,
        /// executando um filtro (expressão).
        /// </summary>
        /// <param name="expressao">Filtro da busca.</param>
        /// <returns>Objeto <see cref="IQueryable"/>.</returns>        
        public static IQueryable<T> Consultar(Expression<Func<T, bool>> expressao)
        {
            var consulta = ObterRepositorio().Consultar(expressao);
            return consulta;
        }

        /// <summary>
        /// Contador de objetos gravados no BD.
        /// </summary>
        /// <returns>Quantidade de objetos salvos.</returns>
        public static int Count()
        {
            var count = ObterRepositorio().Count();
            return count;
        }

        /// <summary>
        /// Contador de objetos gravados no BD e filtrados pela expressão.
        /// </summary>
        /// <param name="expressao">Filtro da busca</param>
        /// <returns>Quantidade de objetos filtrados.</returns>
        public static int Count(Expression<Func<T, bool>> expressao)
        {
            var count = ObterRepositorio().Consultar(expressao).Count();
            return count;
        }

        /// <summary>
        /// Insere o objeto em banco de dados.
        /// </summary>
        public virtual void Inserir()
        {
            Repositorio.Inserir(entidade);
        }

        /// <summary>
        /// Atualiza os dados do objeto no BD.
        /// </summary>
        public virtual void Alterar()
        {
            Repositorio.Alterar(entidade);
        }

        /// <summary>
        /// Insere ou altera os dados do objeto em BD.
        /// </summary>
        public virtual void Gravar()
        {
            Repositorio.Gravar(entidade);
        }

        /// <summary>
        /// Exclui o objeto do banco de dados.
        /// </summary>
        public virtual void Excluir()
        {
            Repositorio.Excluir(entidade);
        }


        /// <summary>
        /// Implementação de igualdade baseado em valores únicos do objeto
        /// </summary>
        /// <param name="obj">Objeto para comparação.</param>
        /// <returns>Valor <see cref="bool"/>.</returns>
        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }

            if ((obj == null) || (obj.GetType() != this.GetType()))
            {
                return false;
            }

            EntidadePersistente<T, K> castObj = (EntidadePersistente<T, K>)obj;

            return (castObj != null) && (this.Id == castObj.Id);
        }

        /// <summary>
        /// Implementação de <see cref="GetHashCode"/> baseado em valores únicos do objeto
        /// </summary>
        /// <returns><see cref="GetHashCode"/> do objeto</returns>
        public override int GetHashCode()
        {
            int hash = 57;
            hash = 27 * hash * Id.GetHashCode();
            return hash;
        }




        /// <summary>
        /// Obtém uma instância do repositório para a entidade.
        /// Este método deve ser usado somente por outros métodos estáticos.
        /// </summary>
        /// <returns><![CDATA[IRepositorio<T>]]>Retorna um repositório</returns>
        protected static K ObterRepositorio()
        {
            var repositorio = Fabrica.Instancia.Obter<K>();
            return repositorio;
        }
    }
}

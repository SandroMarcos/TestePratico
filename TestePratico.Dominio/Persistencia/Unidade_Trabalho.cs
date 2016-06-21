using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using System.Linq.Expressions;


namespace TestePratico.Dominio.Persistencia
{
    public class Unidade_Trabalho : IUnidadeTrabalho
    {

        /// <summary>
        /// Sessão atual da conexão
        /// </summary>
        private ISession sessao;

        /// <summary>
        /// Construtor padrão
        /// </summary>
        public Unidade_Trabalho()
        {
            CriarSessao();
        }

        /// <summary>
        /// Retorna a sessão atual
        /// </summary>
        public ISession Sessao
        {
            get
            {
                if (sessao.Connection.State == System.Data.ConnectionState.Closed)
                {
                    RenovarSession();
                }

                return sessao;
            } 
        }

        /// <summary>
        /// Flag indicando que uma transação está aberta
        /// </summary>
        public bool TransacaoEstaAtiva
        {
            get
            {
                return Sessao.Transaction != null && Sessao.Transaction.IsActive;
            }
        }

        /// <summary>
        /// Gera uma nova sessão para a unidade de trabalho. Tal recurso eh necessário 
        /// para eliminação de cache entre requisições ao modulo server.
        /// </summary>
        public void RenovarSession()
        {
            Dispose();
            CriarSessao();
        }

        /// <summary>
        /// Inicia uma transação no BD.
        /// </summary>
        public void BeginTransaction()
        {
            Sessao.Transaction.Begin();
        }

        /// <summary>
        /// <see cref="Comita"/> a transação corrente.
        /// </summary>
        public void Commit()
        {
            Sessao.Flush();
            Sessao.Transaction.Commit();
            Sessao.Clear();
        }

        /// <summary>
        /// Cancela a transação corrente.
        /// </summary>
        public void Rollback()
        {
            Sessao.Transaction.Rollback();
        }

        /// <summary>
        /// Método para atender a interface <see cref="IDisposable"/>.
        /// </summary>
        public void Dispose()
        {
            sessao.Close();            
            sessao.Dispose();
        }

        /// <summary>
        /// Abertura da sessão.
        /// </summary>
        private void CriarSessao()
        {
            sessao = Conexao.OpenSession();

            ////Sessao.FlushMode = FlushMode.Commit;
            sessao.FlushMode = FlushMode.Never;
        }

    }
}

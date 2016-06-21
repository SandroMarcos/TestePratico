using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestePratico.Dominio.Persistencia
{
    public interface IUnidadeTrabalho : IDisposable
    {
        /// <summary>
        /// Flag indicando que uma transação está aberta
        /// </summary>
        bool TransacaoEstaAtiva { get; }

        /// <summary>
        /// Abertura de uma transação de banco de dados. 
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// <see cref="Commit"/> de uma transação de banco de dados. 
        /// </summary>
        void Commit();

        /// <summary>
        /// <see cref="Rollback"/> de uma transação de banco de dados.
        /// </summary>
        void Rollback();

        /// <summary>
        /// Força o fechamento de uma sessão antiga e a criação de uma nova.
        /// </summary>
        void RenovarSession();
    }
}

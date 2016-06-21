using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using Ninject.Modules;
using Ninject.Parameters;
using TestePratico.Dominio.Persistencia;

namespace TestePratico.Dominio.Infra
{
    public class Fabrica
    {
        private static Fabrica instancia;

        private Fabrica()
        {
            var persistencia = new ModuloNHibernate();
            Kernel = new StandardKernel(new NinjectSettings { LoadExtensions = false });
            Kernel.Load(persistencia);
        }

        public static Fabrica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new Fabrica();
                }

                return instancia;
            }
        }

        public StandardKernel Kernel { get; private set; }

        /// <summary>
        /// Obtém um objeto a partir do mapeamento da injeção de dependência.
        /// </summary>
        /// <typeparam name="T">Tipo de referencia mapeado.</typeparam>
        /// <returns>Tipo de retorno mapeado.</returns>
        public T Obter<T>()
        {
            return Kernel.Get<T>();
        }

        /// <summary>
        /// Retorna o tipo de objeto
        /// </summary>
        /// <typeparam name="T">Classe avaliada</typeparam>
        /// <param name="tipo">Tipo do objeto</param>
        /// <returns>Tipo de retorno mapeado</returns>
        public T Obter<T>(System.Type tipo)
        {
            Parameter[] parameter = null;

            return (T)Kernel.Get(tipo, parameter);
        }

        /// <summary>
        /// Carrega módulos específicos para o funcionamento do Monitor
        /// </summary>
        /// <param name="modulo">Módulo específico para o acionamento de AOP</param>
        public void CarregarModulos(NinjectModule modulo)
        {            
            Kernel.Load(modulo);
        }


    }
}

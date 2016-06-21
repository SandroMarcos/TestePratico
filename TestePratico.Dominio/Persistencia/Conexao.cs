using System.IO;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using TestePratico.Dominio.Classes;

namespace TestePratico.Dominio.Persistencia
{
    public class Conexao
    {
        private static ISessionFactory sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory == null)
                {
                    IPersistenceConfigurer baseDeDados = SQLiteConfiguration.Standard.ConnectionString(
                                  x => x.FromConnectionStringWithKey("TestePratico")).ShowSql().Dialect("NHibernate.Dialect.SQLiteDialect");

                    FluentConfiguration configuration = Fluently.Configure()
                        .Database(baseDeDados)
                        .Mappings(x => x.FluentMappings.AddFromAssemblyOf<Usuario>());

                    try
                    {
                        ExportarEsquema(configuration.BuildConfiguration());
                    }
                    catch
                    {
                    }

                    sessionFactory = configuration.BuildSessionFactory();
                }

                return sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

        /// <summary>
        /// Usado somente para desenvolvedores obterem <see cref="ddl"/>.
        /// </summary>
        /// <param name="configuration">Configuração da <see cref="session do NHibernate"/>.</param>
        private static void ExportarEsquema(Configuration configuration)
        {
            SchemaExport exportaSchema = new SchemaExport(configuration);

            using (TextWriter arquivoSaida = new StreamWriter("d:\\schema.sql"))
            {
                exportaSchema.Execute(true, false, false, null, arquivoSaida);
            }
        }

    }
}

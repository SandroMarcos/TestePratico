using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using TestePratico.Dominio.Classes;

namespace TestePratico.Dominio.Persistencia
{
    public class ModuloNHibernate : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepositorio<Usuario>>().To<Repositorio<Usuario>>();
            Bind<IRepositorio<Skills>>().To<Repositorio<Skills>>();
            Bind<IRepositorio<SkillsUsuarios>>().To<Repositorio<SkillsUsuarios>>();
        }
    }
}

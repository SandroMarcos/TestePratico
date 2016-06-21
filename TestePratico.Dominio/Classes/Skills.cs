using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestePratico.Dominio.Persistencia;

namespace TestePratico.Dominio.Classes
{
    /// <summary>
    /// Classe de Skills
    /// </summary>
    public class Skills : EntidadePersistente<Skills, IRepositorio<Skills>>
    {
        public Skills()
            : base()
        {
        }
        
        public virtual string Descricao { get; set; }

        public virtual IList<SkillsUsuarios> SkillsUsuarios { get; set; }
    }
}

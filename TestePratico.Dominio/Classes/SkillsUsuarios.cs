using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestePratico.Dominio.Persistencia;

namespace TestePratico.Dominio.Classes
{
    /// <summary>
    /// Classe de Skills X Usuários
    /// </summary>
    public class SkillsUsuarios : EntidadePersistente<SkillsUsuarios, IRepositorio<SkillsUsuarios>>
    {
        public SkillsUsuarios()
            : base()
        {
        }        

        public virtual Usuario Usuario { get; set; }

        public virtual Skills Skills { get; set; }

        /// <summary>
        /// Inseri um novo Skills X Usuários
        /// </summary>
        /// <param name="usuario">Usuário do sistema</param>
        /// <param name="skill">Skill selecionado</param>
        public static void InserirSkillUsuario(Usuario usuario, Skills skill)
        {
            (new SkillsUsuarios()
            {
                Skills = skill,
                Usuario = usuario
            }).Gravar();
        }

        /// <summary>
        /// Excluir uma coleção de Skills X Usuários
        /// </summary>
        /// <param name="listagem">Listagem de Skills X Usuários</param>
        public static void ExcluirSkillUsuario(IList<SkillsUsuarios> listagem)
        {
            foreach (var item in listagem)
            {
                item.Excluir();
            }
        }
    }
}

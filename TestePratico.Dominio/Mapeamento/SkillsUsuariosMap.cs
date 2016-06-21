using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using TestePratico.Dominio.Classes;


namespace TestePratico.Dominio.Mapeamento
{
    class SkillsUsuariosMap : ClassMap<SkillsUsuarios>
    {
        public SkillsUsuariosMap()
        {
            Table("SkillsUsuarios");
            LazyLoad();

            Id(x => x.Id).GeneratedBy.Native().Column("CodSkillsUsuarios");
            References(x => x.Usuario).Column("CodUsuario").Not.Nullable().ForeignKey("fkSkillsUsuarios_Usuarios");
            References(x => x.Skills).Column("CodSkill").Not.Nullable().ForeignKey("fkSkillsUsuarios_Skills");
        }
    }
}

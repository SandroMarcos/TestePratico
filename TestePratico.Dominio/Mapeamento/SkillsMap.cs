using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using TestePratico.Dominio.Classes;

namespace TestePratico.Dominio.Mapeamento
{
    public class SkillsMap : ClassMap<Skills>
    {
        public SkillsMap()
        {
            Table("Skills");
            LazyLoad();

            Id(x => x.Id).GeneratedBy.Native().Column("CodSkill");
            Map(x => x.Descricao).Column("Descricao").Not.Nullable();
            HasMany(x => x.SkillsUsuarios).KeyColumn("CodSkill").Inverse();
        }
    }
}

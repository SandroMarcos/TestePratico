using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using TestePratico.Dominio.Classes;

namespace TestePratico.Dominio.Mapeamento
{
    public class UsuarioMap : ClassMap<Usuario>
    {
        public UsuarioMap()
        {
            Table("Usuario");
            LazyLoad();

            Id(x => x.Id).GeneratedBy.Native().Column("CodUsuario");
            Map(x => x.Nome).Column("Nome").Not.Nullable().Length(100);
            Map(x => x.Login).Column("Login").Not.Nullable().Length(50);
            Map(x => x.Senha).Column("Senha").Not.Nullable().Length(100);
            Map(x => x.TipoUsuario).Column("TipoUsuario").CustomType<int>();
            Map(x => x.Administrador).Column("Administrador");
            HasMany(x => x.SkillsUsuarios).KeyColumn("CodUsuario").Inverse();
        }
    }
}

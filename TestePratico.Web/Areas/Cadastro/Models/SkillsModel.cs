using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestePratico.Dominio.Classes;

namespace TestePratico.Web.Areas.Cadastro.Models
{
    public class SkillsModel
    {
        public SkillsModel()
        {

        }

        public int Id { get; set; }

        public string Descricao { get; set; }

        public IList<SkillsModel> listagem { get; set; }

        public static SkillsModel CarregarModel()
        {
            var model = new SkillsModel();
            model.listagem = Skills.Consultar()
                .Select(x => new SkillsModel()
                {
                    Id = x.Id,
                    Descricao = x.Descricao
                }).ToList();

            return model;

        }

        public static SkillsModel CarregarModel(int id)
        {
            var skill = Skills.ObterPorId(id);
            var model = new SkillsModel();
            model.Descricao = skill.Descricao;
            model.Id = skill.Id;
            model.listagem = Skills.Consultar()
                .Select(x => new SkillsModel()
                {
                    Id = x.Id,
                    Descricao = x.Descricao
                }).ToList();

            return model;

        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestePratico.Dominio.Classes;

namespace TestePratico.Web.Areas.Cadastro.Models
{
    public class SkillsUsuariosModel
    {
        public SkillsUsuariosModel()
        {
            CarregarTodosUsuarios();
            CarregarTodosSkills();
            Skills = new List<int>();
        }        

        public int CodUsuario { get; set; }

        public List<int> Skills { get; set; }

        public List<SelectListItem> TodosUsuarios { get; set; }

        public List<SelectListItem> TodosSkills { get; set; }        

        public static SkillsUsuariosModel CarregarModel()
        {
            var model = new SkillsUsuariosModel();
            return model;

        }

        public static SkillsUsuariosModel CarregarModel(int id)
        {
            var model = new SkillsUsuariosModel();
            model.CodUsuario = id;
            model.Skills = SkillsUsuarios.Consultar(x => x.Usuario.Id == id)
                .Select(c => c.Skills.Id).ToList();

            return model;
        }


        /// <summary>
        /// Carrega as situações do atendimento
        /// </summary>
        private void CarregarTodosUsuarios()
        {
            TodosUsuarios = Usuario.Consultar()
                .Select(x => new SelectListItem()
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }).ToList();

        }

        /// <summary>
        /// Carrega as situações do atendimento
        /// </summary>
        private void CarregarTodosSkills()
        {
            TodosSkills = TestePratico.Dominio.Classes.Skills.Consultar()
                .Select(x => new SelectListItem()
                {
                    Text = x.Descricao,
                    Value = x.Id.ToString()
                }).ToList();
        }
    }
}
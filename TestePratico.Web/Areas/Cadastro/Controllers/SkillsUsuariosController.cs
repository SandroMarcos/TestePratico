using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestePratico.Web.Controllers;
using TestePratico.Web.Areas.Cadastro.Models;
using TestePratico.Web.Filtros;
using TestePratico.Dominio.Classes;

namespace TestePratico.Web.Areas.Cadastro.Controllers
{
    [Filtros.ControleAcesso(true)]
    public class SkillsUsuariosController : ControladoraPadrao
    {
        private SkillsUsuariosModel skillSkillsUsuariosModel = null;

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ObterTodos()
        {
            try
            {
                skillSkillsUsuariosModel = SkillsUsuariosModel.CarregarModel();

            }
            catch (Exception)
            {
            }

            return Json(skillSkillsUsuariosModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObterListagem()
        {
            IQueryable<dynamic> listagem = null;

            try
            {
                listagem = Usuario.Consultar()
                    .Select(c => new
                    {
                        Usuario = c.Nome,
                        Skill = c.SkillsUsuarios.Select(m => m.Skills.Descricao)
                    });

            }
            catch (Exception)
            {
            }

            return Json(listagem, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Obter(int id)
        {
            try
            {
                skillSkillsUsuariosModel = SkillsUsuariosModel.CarregarModel(id);
            }
            catch (Exception)
            {
            }

            return Json(skillSkillsUsuariosModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Novo()
        {
            try
            {
                skillSkillsUsuariosModel = SkillsUsuariosModel.CarregarModel();
            }
            catch (Exception)
            {
            }

            return Json(skillSkillsUsuariosModel, JsonRequestBehavior.AllowGet);
        }

        [ControleTransacao]
        public JsonResult Excluir(int id)
        {
            try
            {
                var usuario = Usuario.ObterPorId(id);
                SkillsUsuarios.ExcluirSkillUsuario(usuario.SkillsUsuarios);
            }
            catch (Exception e)
            {
                OkJson = false;
                MensagemJson = e.Message;
            }

            return Json(RetornoJson(), JsonRequestBehavior.AllowGet);
        }

        [ControleTransacao]
        public JsonResult Salvar(SkillsUsuariosModel model)
        {
            try
            {
                var usuario = Usuario.ObterPorId(model.CodUsuario);

                SkillsUsuarios.ExcluirSkillUsuario(usuario.SkillsUsuarios);

                foreach (var item in model.Skills)
                {
                    SkillsUsuarios.InserirSkillUsuario(usuario, Skills.ObterPorId(item));
                }
            }
            catch (Exception e)
            {
                MensagemJson = e.InnerException.ToString();
            }

            return Json(RetornoJson(), JsonRequestBehavior.AllowGet);
        }

    }
}
